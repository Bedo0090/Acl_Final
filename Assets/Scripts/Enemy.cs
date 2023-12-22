using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
enum EnemyWeaponState
{
    Armed,
    Unarmed
}

enum EnemyState
{
    Idle,
    Patrol,
    Dead,
    ApproachPlayer,
    Attack,
    Knocked
}

enum AttackType
{
    None,
    Punch,
    Grapple,
    Swing,
    Throw
}

public class Enemy : MonoBehaviour
{
    float health;
    public Slider healthSlider;
    public GameObject healthBarUI;

    private NavMeshAgent enemy;
    [SerializeField] EnemyState state;
    [SerializeField] EnemyWeaponState weaponState;
    GameObject player;
    public GameObject weapon;
    AttackType attackType;

    float elabsedKnockedTime;

    float timeSinceLastAttack;

    bool grapplingPlayer = false;
    float elabsedGrapplingTime;

    private Animator animator;
    PlayerManager playerManager;

    // death
    public int droppedCoins = 0;

    // grappling
    bool attemptGrapple;
    float elabsedAttemptingGrappleTime;

    //patrolling
    public Transform[] points;
    private int destPoint = 0;

    //throwing weapon 
    bool decisionMade;

    private void Awake()
    {
        player = GameObject.Find("PlayerArmature");
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        healthSlider.value = CalculateHealth();

        elabsedKnockedTime = 0;


        timeSinceLastAttack = Constants.timeBetweenAttacks;

        elabsedGrapplingTime = 0;

        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerManager = player.GetComponent<PlayerManager>();

        attackType = AttackType.None;

        healthBarUI.transform.LookAt(player.transform);

        attemptGrapple = false;
        elabsedAttemptingGrappleTime = 0;

        decisionMade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.Dead)
        {
            this.GetComponent<CapsuleCollider>().enabled = false;
            return;
        }

        HandleHealth();

        if (state != EnemyState.Attack)
        {
            attackType = AttackType.None;
        }

        if (attemptGrapple)
        {
            elabsedAttemptingGrappleTime += Time.deltaTime;

            if (elabsedAttemptingGrappleTime >= Constants.attemptingGrappleMaxTime && !grapplingPlayer)
            {
                StopGrappling();
                elabsedAttemptingGrappleTime = 0;
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HitReaction"))
        {
            return;
        }

        if (grapplingPlayer)
        {
            elabsedGrapplingTime += Time.deltaTime;

            if (elabsedGrapplingTime >= Constants.grapplingTime)
            {
                // stop grappling
                StopGrappling();
                // dmg player
                player.GetComponent<PlayerHealth>().TakeDmg(5);

            }
        }
        else if ((playerManager.GetIsGrappled() && !grapplingPlayer) || playerManager.gameEneded)
        {
            Idle();
        }
        else
        {
            if (state == EnemyState.Knocked)
            {
                elabsedKnockedTime += Time.deltaTime;
                if (elabsedKnockedTime >= Constants.knockedTime)
                {
                    state = EnemyState.Idle;
                    elabsedKnockedTime = 0;
                    animator.SetBool("Knocked", false);
                }
            }
            else
            {
                HandleDecision();
            }
        }
    }

    void HandleDecision()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= Constants.attackDistance)
        {
            Idle();
            animator.SetBool("Run", false);
            if (ReadyToAttack())
                Attack();
        }
        else if (distanceToPlayer < Constants.chaseRange)
        {
            // Player is in chase range

            if (weaponState == EnemyWeaponState.Armed && !decisionMade)
            {
                // make decision about whether to throw or approach

                HandleThrowDecision();
            }
            else
            {
                if (attackType != AttackType.Throw)
                    ApproachPlayer();
            }
        }
        else
        {
            if (weaponState == EnemyWeaponState.Armed)
            {
                if (!enemy.pathPending && enemy.remainingDistance < 0.2f)
                    Patrol();
            }
            else
                Idle();
        }
    }

    void HandleThrowDecision()
    {
        // reset 
        Idle();
        // throw or approach

        int choice = Random.Range(0, 2);
        Debug.Log(choice);
        if (choice == 0)
        {
            state = EnemyState.Attack;
            attackType = AttackType.Throw;
            ReadyToAttack();
            animator.SetBool("Run", false);
            animator.SetTrigger("Throw");
        }
        else
        {
            ApproachPlayer();
        }
        decisionMade = true;


    }

    bool ReadyToAttack()
    {
        // Player is in attack range
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack >= Constants.timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;
            return true;
        }
        else { return false; }
    }
    void HandleHealth()
    {
        healthSlider.value = CalculateHealth();

        if (health < Constants.maxHealth)
        {
            healthBarUI.SetActive(true);
        }
    }

    float CalculateHealth()
    {
        return health / Constants.maxHealth;
    }

    void Idle()
    {
        state = EnemyState.Idle;
        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        transform.LookAt(player.transform);
        //enemy.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        //enemy.GetComponent<NavMeshAgent>().speed = 0;
        enemy.SetDestination(transform.position);

    }

    void ApproachPlayer()
    {
        state = EnemyState.ApproachPlayer;
        animator.SetBool("Run", true);
        transform.LookAt(player.transform);
        //transform.Translate(Vector3.forward * Time.deltaTime);
        //enemy.GetComponent<NavMeshAgent>().speed = 3.5f;
        enemy.SetDestination(player.transform.position);
    }

    void Attack()
    {
        Debug.Log("Attacking");
        state = EnemyState.Attack;

        if (weaponState == EnemyWeaponState.Armed)
        {
            Swing();
        }
        else
        {
            // punch or grapple
            int choice = Random.Range(0, 2);
            if (choice == 0)
            {
                Punch();
            }
            else
            {
                Grapple();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            switch (attackType)
            {
                case AttackType.Grapple:
                    Debug.Log("Grappled");
                    grapplingPlayer = true;
                    playerManager.SetIsGrappled(true, gameObject);
                    attackType = AttackType.None;
                    break;
                case AttackType.Punch:
                    Debug.Log("Punched");
                    player.GetComponent<PlayerHealth>().TakeDmg(1);
                    attackType = AttackType.None;
                    break;
                default: break;
            }

        }
    }

    void Punch()
    {
        Debug.Log("Attempting Punch");
        attackType = AttackType.Punch;
        animator.SetTrigger("Punch");
    }

    void Swing()
    {
        attackType = AttackType.Swing;
        animator.SetTrigger("Swing");
    }

    public void StartSwinging()
    {
        weapon.GetComponent<EnemyWeapon>().SetSwinging(true);
    }
    public void StopSwinging()
    {
        weapon.GetComponent<EnemyWeapon>().SetSwinging(false);
        attackType = AttackType.None;
    }

    void Grapple()
    {
        Debug.Log("Attempting Grapple");
        attemptGrapple = true;
        attackType = AttackType.Grapple;
        animator.SetBool("Grapple", true);
    }

    public void StopGrappling()
    {
        attemptGrapple = false;
        grapplingPlayer = false;
        elabsedGrapplingTime = 0;
        animator.SetBool("Grapple", false);
        playerManager.SetIsGrappled(false, gameObject);
        attackType = AttackType.None;

    }

    public void ThrowWeapon()
    {
        Debug.Log("Throwing Weapon" + state + attackType);
        if (weapon != null)
        {
            // throw weapon
            weapon.GetComponent<EnemyWeapon>().SetThrown(true);

            // Calculate the direction vector from the weapon to the player's position
            Vector3 playerDirection = player.transform.position - weapon.transform.position;

            // Normalize the direction vector to get a unit vector
            Vector3 throwDirection = playerDirection.normalized + Vector3.up * 0.3f;

            //Vector3 forwardVec = transform.forward;
            //Vector3 upVec = transform.up;

            weapon.transform.parent = null;
            weapon.GetComponent<Collider>().isTrigger = false;
            Rigidbody rb = weapon.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            // Calculate the force vector
            Vector3 throwingForceVector = throwDirection * Constants.throwingForce + Vector3.up * Constants.throwUpForce;

            rb.AddForce(throwingForceVector, ForceMode.Impulse);

            weaponState = EnemyWeaponState.Unarmed; weapon = null;
            attackType = AttackType.None;
        }
    }

    public void TakeDmg(int dmg)
    {
        if (health > dmg) // take dmg
        {
            health -= dmg;
            // hit animation
            animator.SetTrigger("Hit");
        }
        else // die
        {
            state = EnemyState.Dead;
            animator.SetTrigger("Die");
            healthBarUI.SetActive(false);
            // drop a random amount of coins from 5 to 50
            droppedCoins = Random.Range(5, 51);
        }
    }

    void Patrol()
    {
        // start animation
        animator.SetBool("Walk", true);
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        transform.LookAt(points[destPoint]);
        enemy.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    public void GetKnocked()
    {
        state = EnemyState.Knocked;
        animator.SetBool("Knocked", true);
    }

}