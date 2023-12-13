using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PistolGun : MonoBehaviour
{
    public int damage;
    public float fireRate, range, reloadTime;
    public int magazineSize;
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
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        if (starterAssetsInputs.shoot && readyToShoot && !reloading && bulletsLeft > 0)
        {
            Shoot();
            starterAssetsInputs.shoot = false;
        }
        if (starterAssetsInputs.reload && bulletsLeft < magazineSize && !reloading)
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
            Debug.Log(rayHit.collider.name);
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
        bulletsLeft = magazineSize;
        Debug.Log(bulletsLeft);
        starterAssetsInputs.shoot = false;
        reloading = false;
    }
}
