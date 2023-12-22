using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int playerHealth;
    // Start is called before the first frame update
    Animator animator;
    PlayerManager playerManager;
    public GameObject healthBarObject;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject health4;
    public GameObject health5;
    public GameObject health6;
    public GameObject health7;
    public GameObject health8;
    public static bool change = false;

    public Material emptyMat;
    public Material fullMat;

    GameObject[] healthBar; 
    void Start()
    {
        playerHealth = player.healthpoints;
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        healthBar = new GameObject[8] {health1, health2, health3 , health4 , health5 , health6 , health7 , health8};
    }

    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            playerHealth = player.healthpoints;
            AssignHealth();
            change = false;
        }
        player.healthpoints = playerHealth;
        
    }

    public void TakeDmg(int dmg)
    {
        if(playerHealth > dmg)
        {
            playerHealth -= dmg;
            animator.SetTrigger("Hit");
        }
        else
        {
            playerHealth = 0;
            GameOver();
        }
        AssignHealth();
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
        AssignHealth();
    }
    void GameOver()
    {
        animator.SetTrigger("Die");
        healthBarObject.SetActive(false);
        playerManager.EndGame();
    }

    void AssignHealth()
    {
        int i = 0;
        // remove health
        for (; i < 8 - playerHealth; i++)
        {
            healthBar[i].GetComponent<MeshRenderer>().material = emptyMat;
        }

        //add health
        for(;i < 8; i++)
        {
            healthBar[i].GetComponent<MeshRenderer>().material = fullMat;

        }
    }
}
