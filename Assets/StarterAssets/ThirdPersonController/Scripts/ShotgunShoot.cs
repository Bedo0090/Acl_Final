using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ShotgunShoot : MonoBehaviour
{
    public int damage;
    public float fireRate, range, reloadTime;
    private int bulletsLeft;

    public StarterAssetsInputs starterAssetsInputs;
    private bool readyToShoot, reloading;

    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;
    public ParticleSystem hitEffectBlood;


    private void Start()
    {
        bulletsLeft = 8;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        if (player.equippedWeapon.name != "Shotgun")
            return;

        player.equippedWeapon.number = bulletsLeft;

        if (starterAssetsInputs.shoot && readyToShoot && !reloading && bulletsLeft > 0)
        {
            Shoot();
            starterAssetsInputs.shoot = false;
            starterAssetsInputs.reload = false;
        }
        if (starterAssetsInputs.reload && bulletsLeft < 8 && !reloading && player.equippedWeaponAmmo != null)
        {
            Reload();
            starterAssetsInputs.reload = false;
        }

    }
    private void Shoot()
    {
        readyToShoot = false;


        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out rayHit, range, whatIsEnemy))
        {
            
            if (rayHit.collider.CompareTag("Enemy"))
            {
                hitEffectBlood.transform.position = rayHit.point;
                hitEffectBlood.transform.forward = rayHit.normal;
                hitEffectBlood.Emit(1);
            }
            else
            {
                hitEffect.transform.position = rayHit.point;
                hitEffect.transform.forward = rayHit.normal;
                hitEffect.Emit(1);
            }
        }
        muzzleFlash.Emit(1);

        bulletsLeft--;
        Debug.Log(bulletsLeft);
        Invoke("ResetShot", fireRate);
    }
    private void ResetShot()
    {
        readyToShoot = true;
        starterAssetsInputs.shoot = false;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        int ammo = player.equippedWeaponAmmo.number;

        if (ammo > 8 - bulletsLeft)
        {
            player.equippedWeaponAmmo.number -= (8 - bulletsLeft);
            bulletsLeft = 8;
        }
        else
        {
            bulletsLeft += ammo;
            player.equippedWeaponAmmo.number = 0;
        }
        starterAssetsInputs.shoot = false;
        reloading = false;
    }
}
