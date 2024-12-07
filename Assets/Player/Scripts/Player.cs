using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private GameObject upGun, midGun, downGun;
    private ParticleSystem upGunVFX, midGunVFX, downGunVFX;

    [SerializeField] private int weaponPower;
    public int WeaponPower
    {
        get
        {
            return weaponPower;
        }
        set
        {
            weaponPower = Mathf.Clamp(value, 0, maxWeaponPower);
        }
    }
    private int maxWeaponPower = 4;

    public static Player Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    public void Start()
    {
        weaponPower = 1;
        upGunVFX = upGun.GetComponent<ParticleSystem>();
        midGunVFX = midGun.GetComponent<ParticleSystem>();
        downGunVFX = downGun.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > fireTime)
        {
            Shooting();
            fireTime = Time.time + 1 / fireRate;
        }
    }

    public override void Shooting()
    {
        switch (weaponPower)
        {
            case 1:
                CreateBullet(Bullet, midGun.transform.position, Vector3.zero);
                midGunVFX.Play();
                break;
            case 2:
                CreateBullet(Bullet, upGun.transform.position, Vector3.zero);
                upGunVFX.Play();
                CreateBullet(Bullet, downGun.transform.position, Vector3.zero);
                downGunVFX.Play();
                break;
            case 3:
                CreateBullet(Bullet, midGun.transform.position, Vector3.zero);
                midGunVFX.Play();
                CreateBullet(Bullet, upGun.transform.position, new Vector3(0, 0, 5));
                upGunVFX.Play();
                CreateBullet(Bullet, downGun.transform.position, new Vector3(0, 0, -5));
                downGunVFX.Play();
                break;
            case 4:
                CreateBullet(Bullet, midGun.transform.position, Vector3.zero);
                midGunVFX.Play();
                CreateBullet(Bullet, upGun.transform.position, new Vector3(0, 0, 5));
                upGunVFX.Play();
                CreateBullet(Bullet, downGun.transform.position, new Vector3(0, 0, -5));
                downGunVFX.Play();
                CreateBullet(Bullet, upGun.transform.position, new Vector3(0, 0, 15));
                upGunVFX.Play();
                CreateBullet(Bullet, downGun.transform.position, new Vector3(0, 0, -15));
                downGunVFX.Play();
                break;
        }
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (Hp <= 0)
        {
            GameManager.Instance.ShowGameEnd();
            Destruction();
        }
    }

    public bool CheckWeaponPower()
    {
        if (weaponPower < maxWeaponPower)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
