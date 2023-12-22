using UnityEngine;
using StarterAssets;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, range, reloadTime;
    public int bulletsPerTap;
    int bulletsLeft;

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
        bulletsLeft = 6;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

    }
    private void MyInput()
    {
        if (player.equippedWeapon.name != "Revolver")
            return;

        player.equippedWeapon.number = bulletsLeft;

        shooting = starterAssetsInputs.shoot;

        if (starterAssetsInputs.reload && bulletsLeft < 6 && !reloading && player.equippedWeaponAmmo != null)
        {
            Reload();
            starterAssetsInputs.reload = false;
        }

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 )
        {  
            Shoot();
            starterAssetsInputs.shoot = false;
            starterAssetsInputs.reload = false;
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
            
            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<Enemy>().TakeDmg(damage);
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

        Invoke("ResetShot", timeBetweenShooting);

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

        if (ammo > 6 - bulletsLeft)
        {
            player.equippedWeaponAmmo.number -= (6 - bulletsLeft);
            bulletsLeft = 6;
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
