using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ARShoot : MonoBehaviour
{
    public int damage;
    public float fireRate, range, reloadTime;
    
    private int bulletsLeft;

    public StarterAssetsInputs starterAssetsInputs;
    private bool readyToShoot, reloading, shooting;

    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;
    public ParticleSystem hitEffectBlood;

    private void Start()
    {
        bulletsLeft = 30;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        shooting = Input.GetKey(KeyCode.Mouse0);

        if (starterAssetsInputs.aim && shooting && readyToShoot && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }
        if (starterAssetsInputs.reload && bulletsLeft < 30 && !reloading)
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
        shooting = false;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        if (Script2.assualtRifleAmmo >= 30 - bulletsLeft)
        {
            Script2.assualtRifleAmmo -= (30 - bulletsLeft);
            bulletsLeft = 30;
        }
        else
        {
            bulletsLeft += Script2.assualtRifleAmmo;
            Script2.assualtRifleAmmo = 0;
        }
        shooting = false;
        reloading = false;
    }
}
