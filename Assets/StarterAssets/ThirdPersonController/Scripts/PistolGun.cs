using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PistolGun : MonoBehaviour
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
        bulletsLeft = 12;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        player.equippedWeapon.number = bulletsLeft;

        if (starterAssetsInputs.shoot && readyToShoot && !reloading && bulletsLeft > 0)
        {
            Shoot();
            starterAssetsInputs.shoot = false;
            starterAssetsInputs.reload = false;
        }
        if (starterAssetsInputs.reload && bulletsLeft < 12 && !reloading && player.equippedWeaponAmmo != null)
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

        if (ammo > 12 - bulletsLeft) {
            player.equippedWeaponAmmo.number -= (12 - bulletsLeft);
            bulletsLeft = 12;
        }
        else
        {
            bulletsLeft += ammo;
            player.equippedWeaponAmmo.number = 0;
            player.inventoryItems.Remove(player.equippedWeaponAmmo);
            player.equippedWeaponAmmo = null;
        }
        starterAssetsInputs.shoot = false;
        reloading = false;
    }
}
