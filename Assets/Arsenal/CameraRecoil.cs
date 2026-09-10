using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    private float recoilAmount;
    private float recoilSpeed;
    private float returnSpeed;
    private Vector3 currentRecoil;
    private Vector3 targetRecoil;
    private Transform transform_;
    private MovementController mc;
    private Vector3 origin;
    private Vector3 prePos;

    private Vector2 screenShake = new Vector2(0, 0);

    void Start()
    {

        origin = transform.localPosition;
        mc = PlayerMovement.Instance.gameObject.GetComponent<MovementController>();    
        prePos = transform.localPosition;
    }


    void Update()
    {
        //Vector3.MoveTowards(transform.localPosition, origin, Time.deltaTime * 0.01f);
        //transform.localPosition = Vector3.Project(-mc.getVelocity(), PlayerHitResponder.Instance.transform.up) * 0.05f + origin;
        screenShake = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.x, 0) * screenShake;
        targetRecoil = Vector3.Lerp(targetRecoil, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRecoil = Vector3.Slerp(currentRecoil, targetRecoil, recoilSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRecoil + new Vector3(screenShake.x, screenShake.y, 0));

        screenShake = Vector2.MoveTowards(screenShake, Vector2.zero, Time.deltaTime * 0.1f);
    }

    void LateUpdate()
    {
        prePos = transform.localPosition;
    }

    public void ApplyRecoil(float recoilAmount_, float recoilSpeed_, float returnSpeed_)
    {
        recoilAmount = recoilAmount_;
        recoilSpeed = recoilSpeed_;
        returnSpeed = returnSpeed_;
        targetRecoil += new Vector3(-recoilAmount, 0, 0);
        //ApplyScreenShake(recoilAmount_);
        
    }

    public void ApplyScreenShake(float screenShakeAmount_)
    {
        screenShake = Vector2.one * screenShakeAmount_ * 0.5f;
    }
}
