using System;
using UnityEngine;

public class Gun : Item, IAmmoHandler
{
    public event Action<int> OnAmmoChanged;
    [SerializeField] private bool allowButtonHold;
    [SerializeField] protected int damage = 20;
    [SerializeField] protected int maxAmmo = 10;
    protected int ammo;

    [SerializeField] private float timeBetweenShooting = 0.1f;
    [SerializeField] private int bulletsPerShot = 1;
    protected bool readyToShoot;
    protected bool shooting;
    protected Animator animator = null;
    protected WeaponRecoil weaponRecoil;
    protected CameraRecoil cameraRecoil;

    void Awake()
    {
        ammo = maxAmmo;
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

    void Update()
    {
        GunInput();
        if (shooting && readyToShoot && ammo > 0)
        {
            readyToShoot = false;
            SubstractAmmo(1);
            for(int i = 0; i < bulletsPerShot; i++)
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

    

    public void AddAmmo(int ammoAmount)
    {
        ammo += Mathf.Clamp(ammoAmount, 0, maxAmmo - ammo);
        AmmoChanged();
    }

    public void SubstractAmmo(int ammoAmount)
    {
        if(ammoAmount == 1)
            ammo--;   
        AmmoChanged();
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public void AmmoChanged()
    {
        OnAmmoChanged?.Invoke(ammo);
    }
}
