using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Projectile : MonoBehaviour
{
    public string projectileName;
    [TextArea]
    public string description;
    public float health;
    public float knockback;
    public float pierceCount;
    public bool pierceAll;
    [Space]
    [Header("DO NOT EDIT")]
    public DamageType damageType;
    public float lifetime;
    public int damage;
    public float speed;
    public GameObject owner;

    [HideInInspector]
    public bool canMove = true;
    
    //public Status 
    //public Effect projEffect
 
    //ProjectileEffect effect;
    Sprite sprite;


    // Start is called before the first frame update
    void Start() 
    {
        
    }

    void Awake()
    {
        // MAKE FINAL CALCULATIONS FOR DAMAGE HERE
        //StartCoroutine(DestroyTimer(lifespan));
    }
    private void FixedUpdate()
    {
        if(canMove) ProjectileMovement(speed);
        DestroyLifespan();
    }
    /// <summary> Called when the projectile collides with an object, but is still continuing.</summary>
    public abstract void OnPierce(Collider2D other);
    /// <summary> Called when the projectile hits its final target and is stopping. </summary>
    public abstract void OnCollide(Collider2D other);
    /// <summary> Applies a status effect </summary>
    public abstract void ApplyEffect();
    /// <summary> Called when the projectile is at the end of its life </summary>
    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }
    public virtual void ProjectileMovement(float speed)
    {
        //WE WANT TO USE PHYSICS INSTEAD :(
        transform.Translate(transform.right * (speed / 100), Space.World);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        GameObject hitRoot = other.transform.root.gameObject;

        if (hitRoot.CompareTag("Enemy"))
        {
            if (pierceAll)
            {
                OnPierce(other);
            }
            else if (pierceCount <= 0)
            {
                OnCollide(other);
            }
            else
            { 
                OnPierce(other);                
                pierceCount--;
            }
            //return;
        }

        if (other.CompareTag("Solid"))
        {
            OnCollide(other);
            return;
        }    
    } 

    public void DisableAllColliders()
    {
        Array.ForEach(gameObject.GetComponentsInChildren<BoxCollider2D>(), o => o.enabled = false);
    }
    public void ParentTo(Transform parentTransform)
    {
        transform.parent = parentTransform;
        canMove = false;
    }
    /// <summary> Resets the lifetime of a projectile to the specified time in seconds </summary>
    public void ResetLifetime(float timeInSeconds)
    {
        lifetime = timeInSeconds;
    }

    /// <summary> Destroys the projectile based on its current lifetime.
    /// It can be extended by using ResetLifetime to reset the projectiles lifetime. </summary>
    public void DestroyLifespan()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            OnDeath();
        }
    }

    /// <summary> Starts a coroutine to be destroyed regardless of events </summary>
    IEnumerator DestroyTimer(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        Destroy(transform.root.gameObject);
    }

    public bool CanBeDamaged(GameObject check)
    {
        try
        {
            CharacterBehavior script = check.gameObject.GetComponent<EnemyBehaviour>();
            if (script != null && !script.isInvincible) return true;
        }catch{
            return false;
        }

        return false;
    }
    
}
