using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    public float recoilAmount;
    public float recoilSpeed;
    public float returnSpeed;
    private Vector3 currentRecoil;
    private Vector3 targetRecoil;
    private Transform transform_;


    void Update()
    {
        targetRecoil = Vector3.Lerp(targetRecoil, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRecoil = Vector3.Slerp(currentRecoil, targetRecoil, recoilSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRecoil);
    }

    public void ApplyRecoil()
    {
        targetRecoil += new Vector3(-recoilAmount, 0, 0);
    }
}
