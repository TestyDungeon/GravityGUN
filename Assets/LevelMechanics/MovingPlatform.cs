using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 start;
    Vector3 end;
    Vector3 to;
    [SerializeField] Vector3 direction = Vector3.right;
    [SerializeField] float distance = 10;
    [SerializeField] float speed = 1;

    void Start()
    {
        start = transform.position;
        end = start + direction * distance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((transform.position-start).sqrMagnitude < 1)
            to = end;
        else if ((transform.position-end).sqrMagnitude < 1)
            to = start;

        transform.position += (to - transform.position).normalized * speed * Time.fixedDeltaTime;
    }
}
