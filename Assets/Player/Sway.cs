using UnityEngine;

public class Sway : MonoBehaviour
{
    [SerializeField] private float cameraSway = 0.3f; 
    [SerializeField] private float velocitySway = 0.1f; 
    private Transform transform_;
    private Vector2 mouseInput = Vector2.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 swayPos;
    private Vector3 swayCamPos;


    void Update()
    {
        transform_ = GetComponent<Inventory>().GetCurrent().gameObject.transform;
        GetInput();
        GetVelocity();
        SwayPosition();
        SwayApply();
    }
    
    private void GetInput()
    {
        mouseInput.x = Input.GetAxisRaw("Mouse X");
        mouseInput.y = Input.GetAxisRaw("Mouse Y");
    }

    private void GetVelocity()
    {
        velocity = GetComponent<PlayerMovement>().get_vel();
    }

    private void SwayPosition()
    {
        swayCamPos.x = -mouseInput.x * cameraSway;
        swayCamPos.y = -mouseInput.y * cameraSway;
        swayPos = -transform_.InverseTransformDirection(velocity) * velocitySway;
    }

    private void SwayApply()
    {
        transform_.localPosition = Vector3.Lerp(transform_.localPosition, swayPos+swayCamPos, Time.deltaTime * 5);
    }
}
