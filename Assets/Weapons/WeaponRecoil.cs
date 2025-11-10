using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private float recoilAmount;
    [SerializeField] private float recoilSpeed;
    [SerializeField] private float returnSpeed;
    private Vector3 currentRecoil;
    private Vector3 targetRecoil;

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
        currentRecoil = Vector3.zero;
        transform.localRotation = Quaternion.Euler(currentRecoil);
        targetRecoil = new Vector3(-recoilAmount, 0, 0);
    }
}
