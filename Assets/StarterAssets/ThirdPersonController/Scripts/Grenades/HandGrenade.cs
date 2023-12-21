using UnityEngine;

public class HandGrenade : Grenade
{
    float explosiveForce = 10;
    protected override void AffectEnemy() {
        Collider[] collidersToDamage = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in collidersToDamage)
        {
            //Damage them
            if (col.gameObject.CompareTag("Enemy"))
            {
                col.gameObject.GetComponent<Enemy>().TakeDmg(4);
            }
            else if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<PlayerHealth>().TakeDmg(4);
                Debug.Log("Player hit");
            }

            //Apply force to them
            //Rigidbody rb = col.GetComponent<Rigidbody>();
            //if(rb != null)
            //{
            //    rb.AddExplosionForce(explosiveForce, transform.position, radius);
            //}
        }

        
    }
}