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
    public Rig aimLayer;

    private void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
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
        if (starterAssetsInputs.aim)
        {
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
            aimVCamera.gameObject.SetActive(false);
            thirdPersonController.SetRotateOnMove(true);
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }

        
    }
}
