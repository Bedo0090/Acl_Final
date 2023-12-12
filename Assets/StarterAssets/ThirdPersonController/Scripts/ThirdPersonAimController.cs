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
    public GameObject pistolObject;
    public GameObject shotgunObject;
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

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ThrowGrenade"))
        {
            for (int i = 0; i <= 3; i++)
            {
                rigLayers[i].active = false;
            }
            pistolObject.SetActive(false);
        }
        // check what is equipped
        else if (weaponEquipped.Equals("pistol"))
        {
            for (int i = 0; i <= 3; i++)
            {
                rigLayers[i].active = true;
            }
            pistolObject.SetActive(true);
        }

        if (starterAssetsInputs.switchWeapon)
        {
            if (aimLayer == aimLayerPistol)
            {
                aimLayer = aimLayerSG;
                pistolObject.SetActive(false);
                shotgunObject.SetActive(true);
                weaponEquipped = "shotgun";
                for (int i = 0; i <= 5; i++)
                {
                    if (i <=2)
                        rigLayers[i].active = false;
                    else
                        rigLayers[i].active = true;
                }
            }
            else
            {
                aimLayer = aimLayerPistol;
                shotgunObject.SetActive(false);
                pistolObject.SetActive(true);
                weaponEquipped = "pistol";
                for (int i = 0; i <= 5; i++)
                {
                    if (i <= 2)
                        rigLayers[i].active = true;
                    else
                        rigLayers[i].active = false;
                }
            }

            starterAssetsInputs.switchWeapon = false;
        }
        if (starterAssetsInputs.aim)
        {
            crossHairUI.SetActive(true);
            aimVCamera.gameObject.SetActive(true);
            thirdPersonController.SetRotateOnMove(false);

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
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }

        
    }
}
