using UnityEngine;

public abstract class Grenade : MonoBehaviour
{
    // flags
    bool hasExploded;
    bool isUnclipped = false;

    // properties
    public float radius;
    const int detonationTime = 3;
    float countDown;

    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        hasExploded = false;
        radius = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUnclipped)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0 && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    void Explode()
    {
        Debug.Log("Explosion!");
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            AffectEnemy();
            Destroy(explosion, 2f);
        }
        else
        {
            Debug.Log("No explosion effect");
        }
        
        Destroy(gameObject);

    }

    public void StartCounter()
    {
        isUnclipped = true;
        countDown = detonationTime;
        Debug.Log("Started");
    }

    protected abstract void AffectEnemy();
}