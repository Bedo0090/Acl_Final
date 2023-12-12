using UnityEngine;

public class FlashGrenade : Grenade
{
    protected override void AffectEnemy() {
        Collider[] collidersToKnock = Physics.OverlapSphere(transform.position, radius);  
        
        foreach (Collider col in collidersToKnock)
        {
            //Knock them down
        }
    }
}