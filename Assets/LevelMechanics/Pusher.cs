using UnityEngine;

public class Pusher : MonoBehaviour
{
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 delta = Vector3.zero;
    private Collider col;
    
    void Awake()
    {
        col = GetComponent<Collider>();
    }


    void FixedUpdate()
    {
        delta = transform.position - lastPosition;
        lastPosition = transform.position;
    }

    public Vector3 getDelta()
    {
        return delta;
    }
}
