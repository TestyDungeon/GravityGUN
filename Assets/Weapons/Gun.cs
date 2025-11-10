using UnityEngine;

public class Gun : Item
{
    [SerializeField] private bool allowButtonHold;
    [SerializeField] protected int damage = 20;
    [SerializeField] private float timeBetweenShooting = 0.1f;
    protected bool readyToShoot;
    protected bool shooting;
    protected Animator animator = null;
    protected WeaponRecoil weaponRecoil;
    protected CameraRecoil cameraRecoil;

    void Awake()
    {
        if (GetComponentInChildren<Animator>())
        {
            animator = GetComponentInChildren<Animator>();
        }
        readyToShoot = true;
    }

    void Start()
    {
        cameraRecoil = player.GetComponentInChildren<CameraRecoil>();
        weaponRecoil = GetComponent<WeaponRecoil>();
    }

    // Update is called once per frame
    void Update()
    {
        GunInput();
        if (shooting && readyToShoot)
        {
            readyToShoot = false;
            Shoot();
            Invoke("ResetShot", timeBetweenShooting);
            cameraRecoil.ApplyRecoil();
            weaponRecoil.ApplyRecoil();
        }
    }

    private void GunInput()
    {
        if (allowButtonHold)
            shooting = Input.GetButton("Fire1");
        else
            shooting = Input.GetButtonDown("Fire1");

    }
    
    private void ResetShot()
    {
        readyToShoot = true;
    }

    protected virtual void Shoot()
    {

    }
}
