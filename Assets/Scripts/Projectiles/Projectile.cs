using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public string projectileName;
    public float damage;
    public float speed;
    public float health;
    public float knockback;
    public float lifespan = 1;
    public float pierceCount = 1; 
    
    //public Status 
    //public Effect projEffect
 
    //ProjectileEffect effect;
    Sprite sprite;


    // Start is called before the first frame update
    void Start() { }

    void Awake()
    {
        // MAKE FINAL CALCULATIONS FOR DAMAGE HERE
        StartCoroutine(DestroyDelay(lifespan));
    }

    // Update is called once per frame
    void Update()
    {
        // Do not make anything in fixed/update unless ABSOLUTELY necessary.
    }

    private void FixedUpdate()
    {
        MoveProjectile(speed);
  
    }

    //TODO: Why is the projectile overroating??!

    public virtual void MoveProjectile(float speed)
    {
        transform.Translate(transform.right * (speed / 100), Space.World);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy")){
        //  other.gameObject.ApplyDamage;
        }

        if (!other.gameObject.CompareTag("Player"))
        {
            pierceCount--;
            //other.TryGetComponent<EnemyManager>().ApplyDamage(damage);

            if (pierceCount < 0)
            {
                Destroy(gameObject);
            }

        }
    }

    IEnumerator DestroyDelay(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        Destroy(transform.root.gameObject);
    }
}
