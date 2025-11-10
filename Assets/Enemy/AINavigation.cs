using UnityEngine;
using UnityEngine.AI;

public class AINavigation
{
    //private NavMeshPath path = new NavMeshPath();

    public Vector3 CalculatePath(Vector3 from, Vector3 to)
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);
        if (path.corners.Length > 1)
            return (path.corners[1] - from).normalized;
        return Vector3.zero;
    }



}
