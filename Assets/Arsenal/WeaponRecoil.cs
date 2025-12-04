using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [Header("Weapon Recoil")]
    [SerializeField] private float recoilAmount;
    [SerializeField] private float recoilSpeed;
    [SerializeField] private float returnSpeed;
    
    [Header("Camera Recoil")]
    [SerializeField] private float recoilAmountCamera;
    [SerializeField] private float recoilSpeedCamera;
    [SerializeField] private float returnSpeedCamera;

    private Vector3 currentRecoil;
    private Vector3 targetRecoil;
    private CameraRecoil camRecoil;

    void Start()
    {
        camRecoil = FindAnyObjectByType<CameraRecoil>();
        
    }

    void Update()
    {
        CalculateRecoil();

    }

    void CalculateRecoil()
    {
        targetRecoil = Vector3.Lerp(targetRecoil, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRecoil = Vector3.Slerp(currentRecoil, targetRecoil, recoilSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRecoil);
    }

    

    public void ApplyRecoil()
    {
        camRecoil.recoilAmount = recoilAmountCamera;
        camRecoil.recoilSpeed = recoilSpeedCamera;
        camRecoil.returnSpeed = returnSpeedCamera;

        currentRecoil = Vector3.zero;
        transform.localRotation = Quaternion.Euler(currentRecoil);
        targetRecoil = new Vector3(-recoilAmount, 0, 0);
    }
}
