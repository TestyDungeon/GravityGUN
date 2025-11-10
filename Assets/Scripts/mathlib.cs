using UnityEngine;

public class mathlib : MonoBehaviour
{
    public static void VectorScale(Vector3 in_, float scale, Vector3 out_)
    {
        out_[0] = in_[0] * scale;
        out_[1] = in_[1] * scale;
        out_[2] = in_[2] * scale;
    }

    public static Vector3 ClampMagnitude(Vector3 in_, float max, float min)
    {
        if (in_.magnitude > max)
            in_ = in_.normalized * max;
        else if (in_.magnitude < min)
            in_ = in_.normalized * min;

        return in_;
    }
    
    public static Vector3 ProjectOnPlaneOblique(Vector3 velocity, Vector3 planeNormal, Vector3 direction)
    {
        Vector3 n = planeNormal.normalized;
        Vector3 d = direction.normalized;

        float denom = Vector3.Dot(d, n);
        if (Mathf.Abs(denom) < 1e-6f)
        {
            return velocity - Vector3.Dot(velocity, n) * n;
        }

        return velocity - (Vector3.Dot(velocity, n) / denom) * d;
    }
}