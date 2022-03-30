using System.Collections;
using UnityEngine;

public abstract class Weapon : BaseItem
{

    public CombatManager manager;
    public GameObject primarySpawn;
    public GameObject secondarySpawn;
    public Animation primaryAnimation;
    public Animation secondaryAnimation;
    // stats
    
    public float projectileHealth;
    public float knockback;

    public float primaryDamage;
    public float secondaryDamage;
    [Tooltip ("Attacks per second (1/DPS)")]
    public float primaryAttackRate;
    [Tooltip("Attacks per second (1/DPS)")]
    public float secondaryAttackRate;

    public float attackRateMultiplier;

    // functionality
    private bool cooldownReady = true;
    public bool isPrimary;


    //ProjectileEffect effect;
    //Sprite sprite;

    public abstract void PrimaryAbility();
    public abstract void SecondaryAbility();


    public void UsePrimaryAbility() {

        if (cooldownReady)
        {
            PrimaryAbility();
            cooldownReady = false;
            StartCoroutine(DelayCooldown(primaryAttackRate));
        }
        else
        {
            // do cooldown stuff
        }

    }
    public void UseSecondaryAbility()
    {
        if (cooldownReady)
        {
            SecondaryAbility();
            cooldownReady = false;
            StartCoroutine(DelayCooldown(secondaryAttackRate));
        }
        else
        {
            //do cooldown stuff
        }
    }
    public void OnPickup()
    {
        pickedUp = true;
        manager = transform.root.GetComponent<CombatManager>();
        primarySpawn = manager.primaryProjectileSpawn;
        secondarySpawn = manager.secondaryProjectileSpawn;
        UpdateAttackSpeed();
        
    }
    public void OnDrop()
    {
        manager = null;
        StartCoroutine(DelayEnable(1f));
    }

    public float UpdateAttackSpeed()
    {
        return attackRateMultiplier = manager.totalAttackSpeedMultiplier;
    }
    public IEnumerator DelayCooldown(float attackRate)
    {
        UpdateAttackSpeed();
        cooldownReady = false;
        float timeInSeconds = 1 / (attackRate*attackRateMultiplier);
        Debug.Log("Cooldown is: " + timeInSeconds);
        yield return new WaitForSeconds(timeInSeconds);  
        cooldownReady = true;
    }
    public IEnumerator DelayEnable(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        transform.Find("Collider").GetComponent<BoxCollider2D>().enabled = true;
        pickedUp = false;
    }
}
