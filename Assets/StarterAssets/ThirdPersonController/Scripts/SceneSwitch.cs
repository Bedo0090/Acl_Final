using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class SceneSwitch : MonoBehaviour
{
    public static bool inventorySceneActive;
    public GameObject inventoryCanvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventorySceneActive)
            {
                Time.timeScale = 1;
                inventoryCanvas.SetActive(false);
                inventorySceneActive = false;
                StarterAssetsInputs.SetCursorState(true);
            }
            else
            {
                Time.timeScale = 0;
                inventoryCanvas.SetActive(true);
                inventorySceneActive = true;
                StarterAssetsInputs.SetCursorState(false);
            }
        }
    }
}
