using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;


public class PlayerManager : MonoBehaviour
{
    public static bool isGrappled = false;
    Animator animator;
    public static bool gameEneded;
    public static bool gameWin;
    GameObject grapplerEnemy;
    public GameObject Door;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject HeartDoor;
    public GameObject SpadeDoor;
    public GameObject ClubDoor;
    public GameObject DiamondDoor;
    public GameObject KeyCardDoor;

    public GameObject gameoverCanvas; 

    // Start is called before the first frame update
    void Start()
    {
        gameWin = false;
        gameEneded = false;
        animator = GetComponent<Animator>();
        grapplerEnemy = null;
    }

    void Update()
    {
        if (gameEneded == true)
        {
            if (gameWin == false)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("FinalState"))
                {
                    gameoverCanvas.SetActive(true);
                    Time.timeScale = 0;
                    StarterAssetsInputs.SetCursorState(false);
                }
            }
            else
            {
                gameoverCanvas.SetActive(true);
                Time.timeScale = 0;
                StarterAssetsInputs.SetCursorState(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Destroy(Door);
            Destroy(Door1);
            Destroy(Door2);
            Destroy(ClubDoor);
            Destroy(SpadeDoor);
            Destroy(HeartDoor);
            Destroy(DiamondDoor);
            Destroy(KeyCardDoor);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            gameObject.GetComponent<PlayerHealth>().GainHealth(4);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            gameObject.GetComponent<PlayerHealth>().ToggleInvincibility();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            player.coins += 1000;
        }
    }

    public bool GetIsGrappled()
    {
        return isGrappled;
    }

    public void SetIsGrappled(bool val, GameObject enemy)
    {
        isGrappled = val;
        animator.SetBool("Grappled", val);
        if (val)
        {
            transform.LookAt(enemy.transform);
            grapplerEnemy = enemy;
        }
    }

    public void StopEnemyGrappling()
    {
        grapplerEnemy.GetComponent<Enemy>().StopGrappling();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            if (other.GetComponent<EnemyWeapon>().IsSwinging())
            {
                gameObject.GetComponent<PlayerHealth>().TakeDmg(2);
                other.GetComponent<EnemyWeapon>().SetSwinging(false);
            }
           
        }
    }
    public void EndGame()
    {
        gameEneded = true;
    }

    
}
