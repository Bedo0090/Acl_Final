using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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

    // Start is called before the first frame update
    void Start()
    {
        currGrenadeType = GrenadeType.None;
        animator = GetComponent<Animator>();
        throwingForce = 5;
        throwUpForce = 5;
        isGrappled  = false;
    }

    // Update is called once per frame
    void Update()
    {
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

                isGrappled = false;
            }

            currGrenadeType = GrenadeType.None;

        }
    }

    public void EquipHandGrenade()
    {
        currGrenadeType = GrenadeType.Hand;
    }

    public void EquipFlashGrenade()
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
