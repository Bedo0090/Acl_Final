using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrenadeType
{
    Flash,
    Hand,
    None
}
public class PlayerGrenade : MonoBehaviour
{
   
    public static GrenadeType currGrenadeType;

    public GameObject flashGrenadePrefab;
    public GameObject handGrenadePrefab;
    public GameObject playerHandRef;
    public GameObject playerHeadRef;

    public GameObject currGrenade;

    private Animator animator;

    public float throwingForce;
    public float throwUpForce;
    Vector3 forwardVec = Vector3.zero;
    Vector3 upVec = Vector3.zero;

    public bool isGrappled;
    PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        currGrenadeType = GrenadeType.None;
        animator = GetComponent<Animator>();
        throwingForce = 5;
        throwUpForce = 5;
        isGrappled  = false;
        playerManager = GetComponent<PlayerManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.equippedGrenade != null)
        {
            if (player.equippedGrenade.name.Equals("Hand Grenade"))
                currGrenadeType = GrenadeType.Hand;
            else
                currGrenadeType = GrenadeType.Flash;
        }
        else
        {
            currGrenadeType = GrenadeType.None;
        }

        isGrappled = playerManager.GetIsGrappled();
        if (Input.GetKeyDown(KeyCode.G) && currGrenadeType!=GrenadeType.None && animator.GetBool("Jump") != true)
        {
            forwardVec = playerHeadRef.transform.forward;
            upVec = playerHeadRef.transform.up;

            if (!isGrappled)
            {
                throwingForce = 5;
                throwUpForce = 5;
                animator.SetTrigger("ThrowGrenade");

                GameObject grenade = Instantiate((currGrenadeType == GrenadeType.Flash) ? flashGrenadePrefab : handGrenadePrefab, playerHandRef.transform.position, playerHandRef.transform.rotation);
                grenade.transform.parent = playerHandRef.transform;
                currGrenade = grenade;

            }
            else
            {
                throwingForce = 0;
                throwUpForce = 0;

                GameObject grenade = Instantiate((currGrenadeType == GrenadeType.Flash)? flashGrenadePrefab : handGrenadePrefab, playerHandRef.transform.position, playerHandRef.transform.rotation);
                grenade.transform.parent = playerHandRef.transform;
                currGrenade = grenade;
                
                StartCounter();
                ApplyForce();

                playerManager.SetIsGrappled(false, null);
                playerManager.StopEnemyGrappling();

            }

            currGrenadeType = GrenadeType.None;
            player.inventoryItems.Remove(player.equippedGrenade);
            player.equippedGrenade = null;

        }
    }

    public static void EquipHandGrenade()
    {
        currGrenadeType = GrenadeType.Hand;
    }

    public static void EquipFlashGrenade()
    {
        currGrenadeType = GrenadeType.Flash;
    }

    public void StartCounter()
    {
        if(currGrenade != null)
        {
            currGrenade.GetComponent<Grenade>().StartCounter();
        }
        else
        {
            Debug.Log("yikes");
        }
    }
    public void ApplyForce()
    {
        if(currGrenade != null)
        {
            currGrenade.transform.parent = null;
            // Apply force to the object
            Rigidbody rb = currGrenade.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.AddForce(forwardVec * throwingForce + upVec * throwUpForce, ForceMode.Impulse);
            Debug.Log(forwardVec);

        }

    }
}
