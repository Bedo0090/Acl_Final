using UnityEngine;

public class HandGrenade : Grenade
{
    float explosiveForce = 2;
    protected override void AffectEnemy() {
        Collider[] collidersToDamage = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in collidersToDamage)
        {
            //Damage them

            //Apply force to them
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosiveForce, transform.position, radius);
            }
        }

        
    }
}