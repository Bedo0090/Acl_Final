using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainmenu : MonoBehaviour
{

    public void game()
    {
        player.RestartGame();

    }
    public void option1()
    {
        SceneManager.LoadScene("options");

    }
    public void Quit()
    {
        Application.Quit();
    }
    public void back() {
        SceneManager.LoadScene("Main menu");

    }
    public void howtoplay() {
        SceneManager.LoadScene("how to play");

    }
    public void credits() {
        SceneManager.LoadScene("credits");
    }

}