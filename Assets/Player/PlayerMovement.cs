using UnityEngine;
using System.Collections.Generic;



public class PlayerMovement : MonoBehaviour, ICustomTriggerReceiver
{
    [SerializeField] private Transform playerPivot;

    private MovementController movementController;

    

    [SerializeField] private float MAX_SPEED = 0.5f;
    [SerializeField] private float accel = 11f;
    [SerializeField] private float friction = 5f;
    [SerializeField] private float speed = 0.35f;
    [SerializeField] private float airMaxSpeed = 0.1f;
    [SerializeField] private float jumpStrength = 0.35f;
    [SerializeField] private float stopSpeed = 0.1f;

    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 externalVelocity = Vector3.zero;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        AirMove();
        JumpButton();
    }

    private void FixedUpdate()
    {
        playerVelocity = movementController.Move(playerVelocity);
    }


    private void AirMove()
    {
        Vector3 wishdir = new Vector3();
        Vector3 wishvel = new Vector3();
        float wishspeed;

        Vector3 forward = new Vector3();
        Vector3 right = new Vector3();

        float fmove, smove;

        forward = playerPivot.forward;
        right = playerPivot.right;

        //Debug.DrawLine(player_pivot.position, player_pivot.position + player_pivot.forward, Color.black);

        fmove = Input.GetAxisRaw("Horizontal");
        smove = Input.GetAxisRaw("Vertical");

        Vector3.Normalize(forward);
        Vector3.Normalize(right);

        for (int i = 0; i < 3; i++)
            wishvel[i] = forward[i] * smove + right[i] * fmove;

        wishdir = wishvel;
        wishspeed = wishdir.magnitude * speed;
        Vector3.Normalize(wishdir);

        //Debug.DrawLine(player_pivot.position, player_pivot.position + wishdir, Color.magenta);

        if (wishspeed > MAX_SPEED)
        {
            mathlib.VectorScale(wishvel, MAX_SPEED / wishspeed, wishvel);
            wishspeed = MAX_SPEED;
        }

        if (movementController.GroundCheck())
        {
            Friction();
            Accelerate(wishdir, wishspeed);
        }
        else
        {
            AirAccelerate(wishdir, wishspeed);
        }
    }

    private void Accelerate(Vector3 wishDir, float wishSpeed)
    {
        float currentSpeed, addSpeed, accelSpeed;

        currentSpeed = Vector3.Dot(playerVelocity, wishDir);
        addSpeed = wishSpeed - currentSpeed;

        if (addSpeed <= 0)
            return;

        accelSpeed = accel * Time.deltaTime * wishSpeed;

        if (accelSpeed > addSpeed)
            accelSpeed = addSpeed;

        for (int i = 0; i < 3; i++)
            playerVelocity[i] += wishDir[i] * accelSpeed;
    }

    private void AirAccelerate(Vector3 wishDir, float wishSpeed)
    {
        float wishSpd = wishSpeed;

        if (wishSpd > airMaxSpeed)
            wishSpd = airMaxSpeed;

        float currentSpeed = Vector3.Dot(playerVelocity, wishDir);
        float addSpeed = wishSpd - currentSpeed;

        if (addSpeed <= 0)
            return;

        float accelSpeed = accel * Time.deltaTime * wishSpeed;

        if (accelSpeed > addSpeed)
            accelSpeed = addSpeed;

        for (int i = 0; i < 3; i++)
            playerVelocity[i] += wishDir[i] * accelSpeed;
    }

    private void Friction()
    {
        //ref float vel;
        float control, drop, newspeed;

        float speed = playerVelocity.magnitude;

        if (speed < 0.01)
        {
            playerVelocity = Vector3.zero;
            return;
        }

        drop = 0;

        if (movementController.GroundCheck())
        {
            control = speed < stopSpeed ? stopSpeed : speed;
            drop += control * friction * Time.deltaTime;
        }

        newspeed = speed - drop;
        if (newspeed < 0)
            newspeed = 0;
        newspeed /= speed;

        //Debug.Log("Newspeed: " + newspeed);
        playerVelocity[0] *= newspeed;
        playerVelocity[1] *= newspeed;
        playerVelocity[2] *= newspeed;
    }

    private void JumpButton()
    {
        if (!movementController.GroundCheck())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity -= Vector3.Project(playerVelocity, transform.up);
            append_vel(transform.up * jumpStrength);
            //Jumped = true;
        }
    }

    //public void OnCustomTriggerEnter(Collider other)
    //{
    // 
    //}
//
    //public void OnCustomTriggerStay(Collider other)
    //{
//
    //}
//
    //public void OnCustomTriggerExit(Collider other)
    //{
    //    
    //}

    public Vector3 get_vel()
    {
        return playerVelocity;
    }

    public void append_vel(Vector3 v)
    {
        if(!float.IsNaN(v.x) && !float.IsNaN(v.y) && !float.IsNaN(v.z))
            playerVelocity += v;
    }
    public void reset_vel()
    {
        playerVelocity = Vector3.zero;
    }

    private void OnGUI()
    {
        GUI.color = Color.green;
        var ups = playerVelocity;
        GUI.Label(new Rect(0, 15, 400, 100),
        "Speed: " + Mathf.Round(ups.magnitude * 100) / 100 + "ups\n" +
        "Velocity: " + ups + "\n" +
        "Grounded: " + movementController.GroundCheck());
    }
}