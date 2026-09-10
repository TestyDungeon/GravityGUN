using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshWarp : MonoBehaviour
{
    [Header("Noise settings")]
    public float noiseScale = 1.5f;      // spatial frequency — higher = more chaotic detail
    public float noiseSpeed = 1.0f;      // how fast it writhes over time
    public float amplitude = 0.15f;      // how far vertices displace

    [Header("Extra chaos")]
    public float pulseSpeed = 3f;        // fast twitchy pulsing on top
    public float pulseAmount = 0.05f;

    Mesh mesh;
    Vector3[] originalVerts;
    Vector3[] workingVerts;
    Vector3[] normals;
    
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVerts = mesh.vertices;
        normals = mesh.normals;
        workingVerts = new Vector3[originalVerts.Length];
    }

    void Update()
    {
        float t = Time.time;

        for (int i = 0; i < originalVerts.Length; i++)
        {
            Vector3 v = originalVerts[i];

            // 3D-ish noise using offset Perlin samples per axis
            float nx = Mathf.PerlinNoise(v.y * noiseScale + t * noiseSpeed, v.z * noiseScale) - 0.5f;
            float ny = Mathf.PerlinNoise(v.x * noiseScale + t * noiseSpeed, v.z * noiseScale + 100) - 0.5f;
            float nz = Mathf.PerlinNoise(v.x * noiseScale + t * noiseSpeed, v.y * noiseScale + 200) - 0.5f;

            Vector3 noiseOffset = new Vector3(nx, ny, nz) * amplitude;

            // sharp twitchy pulse along the normal, using vertex index to desync each point
            float pulse = Mathf.Sin(t * pulseSpeed /* GameManager.Instance.GetCorruptionLevel()*/ + i * 0.7f) * pulseAmount;
            Vector3 pulseOffset = normals[i] * pulse;

            workingVerts[i] = v + noiseOffset + pulseOffset;
        }

        mesh.vertices = workingVerts;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}