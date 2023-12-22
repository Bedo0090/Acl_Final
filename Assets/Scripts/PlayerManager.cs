using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool isGrappled = false;
    Animator animator;
    public bool gameEneded = false;
    GameObject grapplerEnemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        grapplerEnemy = null;
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
