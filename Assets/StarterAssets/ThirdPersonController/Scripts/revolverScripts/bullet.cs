using UnityEngine;
using StarterAssets;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, range, reloadTime, timeBetweenShots;
    public int magazineSize , bulletsPerTap;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public StarterAssetsInputs starterAssetsInputs;

    //Graphics
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;
    public ParticleSystem hitEffectBlood;



    private void Awake()
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
        shooting = starterAssetsInputs.shoot;

        if (starterAssetsInputs.reload && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
            starterAssetsInputs.reload = false;
        }

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 )
        {
            bulletsShot = bulletsPerTap;
           
            Shoot();
            starterAssetsInputs.shoot = false;
        }
    }
    private void Shoot()
    {
        readyToShoot = false;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        //RayCast
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
        bulletsShot--;

        Debug.Log(bulletsLeft);

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
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
