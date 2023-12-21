using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    
    bool swinging;
    bool thrown;

    float timeToDestroy;
    float elabsedTimeToDestroy;
    void Start()
    {
        swinging = false;
        thrown = false;

        timeToDestroy = 5;
        elabsedTimeToDestroy = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        if (thrown)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.collider.GetComponent<PlayerHealth>().TakeDmg(3);
                thrown = false;

            }
        }
       
    }

    private void Update()
    {
        if (thrown)
        {
            elabsedTimeToDestroy += Time.deltaTime;
            if(elabsedTimeToDestroy >= timeToDestroy) {
                Destroy(gameObject);
            }
        }
    }

    public bool IsSwinging()
    {
        return swinging;
    }
    public void SetSwinging(bool val)
    {
        swinging = val;
    }

    public bool IsThrown()
    {
        return thrown;
    }
    public void SetThrown(bool val)
    {
        thrown = val;
    }
}
