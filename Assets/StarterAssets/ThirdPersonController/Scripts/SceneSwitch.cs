using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class SceneSwitch : MonoBehaviour
{
    public static bool inventorySceneActive;
    public static bool storeSceneActive;
    public GameObject inventoryCanvas;
    public GameObject storeCanvas;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
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

    public void MainMenuSwitch()
    {
        SceneManager.LoadScene("Main menu");
    }
}
