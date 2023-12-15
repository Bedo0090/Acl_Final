using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.Animations.Rigging;

public class ThirdPersonAimController : MonoBehaviour
{
    public float aimDuration;

    public CinemachineVirtualCamera aimVCamera;
    public LayerMask aimCollideLayer = new LayerMask();
    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    public Rig aimLayerPistol;
    public Rig aimLayerSG;
    public Rig aimLayerRevolver;
    public GameObject pistolObject;
    public GameObject shotgunObject;
    public GameObject revolverObject;
    public GameObject knifeObject;
    public GameObject crossHairUI;
    private Rig aimLayer;
    private RigBuilder rigBuilder;
    private List<RigLayer> rigLayers;
    private Animator animator;

    private static string weaponEquipped;

    private void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        rigBuilder = GetComponent<RigBuilder>();
        rigLayers = rigBuilder.layers;
        aimLayer = aimLayerPistol;
        weaponEquipped = "pistol";
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mouseWorldPos = Vector3.zero;

        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimCollideLayer))
        {
            mouseWorldPos = raycastHit.point;
        }

        // check what is equipped
        if (weaponEquipped.Equals("pistol"))
        {
            //check if grenade animation is true
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ThrowGrenade"))
            {
                for (int i = 0; i <= 2; i++)
                {
                    rigLayers[i].active = false;
                }
                pistolObject.SetActive(false);
            }
            else
            {
                for (int i = 0; i <= 2; i++)
                {
                    rigLayers[i].active = true;
                }
                pistolObject.SetActive(true);
                aimLayer = aimLayerPistol;
            }
        }
        else if (weaponEquipped.Equals("shotgun"))
        {
            //check if grenade animation is true
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ThrowGrenade"))
            {
                for (int i = 3; i <= 5; i++)
                {
                    rigLayers[i].active = false;
                }
                shotgunObject.SetActive(false);
            }
            else
            {
                for (int i = 3; i <= 5; i++)
                {
                    rigLayers[i].active = true;
                }
                shotgunObject.SetActive(true);
                aimLayer = aimLayerSG;
            }
        }
        else if (weaponEquipped.Equals("revolver"))
        {
            //check if grenade animation is true
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ThrowGrenade"))
            {
                for (int i = 6; i <= 8; i++)
                {
                    rigLayers[i].active = false;
                }
                revolverObject.SetActive(false);
            }
            else
            {
                for (int i = 6; i <= 8; i++)
                {
                    rigLayers[i].active = true;
                }
                revolverObject.SetActive(true);
                aimLayer = aimLayerRevolver;
            }
        }
        else if (weaponEquipped.Equals("knife"))
        {
            //check if grenade animation is true
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ThrowGrenade"))
            {
                knifeObject.SetActive(false);
            }
            else
            {
                knifeObject.SetActive(true);
                aimLayer = null;
            }
        }


        // switch over weapons for testing
        if (starterAssetsInputs.switchWeapon)
        {
            starterAssetsInputs.shoot = false;
            starterAssetsInputs.reload = false;

            if (weaponEquipped.Equals("pistol"))
            {
                pistolObject.SetActive(false);
                for (int i = 0; i <= 2; i++)
                {
                    rigLayers[i].active = false;
                }
                weaponEquipped = "shotgun";
            }
            else if (weaponEquipped.Equals("shotgun"))
            {
                shotgunObject.SetActive(false);
                for (int i = 3; i <= 5; i++)
                {
                    rigLayers[i].active = false;
                }
                weaponEquipped = "revolver";
            }
            else if (weaponEquipped.Equals("revolver"))
            {
                revolverObject.SetActive(false);
                for (int i = 6; i <= 8; i++)
                {
                    rigLayers[i].active = false;
                }
                weaponEquipped = "knife";
            }
            else
            {
                knifeObject.SetActive(false);
                weaponEquipped = "pistol";
            }
            starterAssetsInputs.switchWeapon = false;
        }

        // aim for any weapon
        if (starterAssetsInputs.aim)
        {
            crossHairUI.SetActive(true);
            aimVCamera.gameObject.SetActive(true);
            thirdPersonController.SetRotateOnMove(false);

            if (aimLayer != null)
                aimLayer.weight += Time.deltaTime / aimDuration;

            Vector3 worldAimTarget = mouseWorldPos;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            crossHairUI.SetActive(false);
            aimVCamera.gameObject.SetActive(false);
            thirdPersonController.SetRotateOnMove(true);
            if (aimLayer != null)
                aimLayer.weight -= Time.deltaTime / aimDuration;
        }

        
    }
}
