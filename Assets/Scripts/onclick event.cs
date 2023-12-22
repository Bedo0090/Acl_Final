using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class onclickevent : MonoBehaviour
{
    public void Restart()
    {
        player.RestartGame();
    }
   
    public void Quitthegame() { 
        
        SceneManager.LoadScene("Main menu");

    }
}