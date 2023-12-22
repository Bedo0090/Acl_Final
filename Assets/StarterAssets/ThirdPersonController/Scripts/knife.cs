using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{

    [SerializeField] GameObject myPlayer;
    Animator playerAnimator;
    bool isGrappled;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = myPlayer.GetComponent<Animator>();
        isGrappled = myPlayer.GetComponent<PlayerManager>().GetIsGrappled();
    }

    // Update is called once per frame
    void Update()
    {
        isGrappled = myPlayer.GetComponent<PlayerManager>().GetIsGrappled();
        if (Input.GetKeyDown(KeyCode.F) && player.durability >= 2 && isGrappled)
        {
            Collider[] collidersToDamage = Physics.OverlapSphere(myPlayer.transform.position, 5);

            foreach (Collider col in collidersToDamage)
            {
                //Damage them
                if (col.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = col.gameObject.GetComponent<Enemy>();
                    if (enemy.grapplingPlayer)
                    {
                        playerAnimator.SetTrigger("knife1");
                        player.durability -= 2;
                        enemy.StopGrappling();
                        break;
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.F) && player.durability >= 1)
        {
            Collider[] collidersToDamage = Physics.OverlapSphere(myPlayer.transform.position, 1);
            double minDistance = 0.0;
            Enemy minIndex = null;
            int i = 0;
            foreach (Collider col in collidersToDamage)
            {
                //Damage them
                if (col.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = col.gameObject.GetComponent<Enemy>();
                    if (enemy.state == EnemyState.Knocked)
                    {
                        double distance = Vector3.Distance(col.gameObject.transform.position, player.playerTransform.position);
                        if (i == 0)
                        {
                            minDistance = distance;
                            minIndex = enemy;
                        }
                        else if (distance < minDistance)
                        {
                            minDistance = distance;
                            minIndex = enemy;
                        }
                        i++;

                    }
                }
            }
            if (minIndex != null)
            {
                playerAnimator.SetTrigger("knife2");
                player.durability -= 1;
                minIndex.TakeDmg(100);
            }


        }

    }


}
