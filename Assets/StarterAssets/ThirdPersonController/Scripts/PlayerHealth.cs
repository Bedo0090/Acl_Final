using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(int dmg)
    {
        if(playerHealth > dmg)
        {
            playerHealth -= dmg;
        }
        else
        {
            GameOver();
        }
    }

    public void GainHealth(int healthPoints) {
        if(playerHealth+healthPoints <= 8)
        {
            playerHealth += healthPoints;
        }
        else
        {
            playerHealth = 8;
        }
    }
    void GameOver()
    {

    }
}
