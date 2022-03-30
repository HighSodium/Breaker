using System.Collections;
using UnityEngine;

public abstract class Weapon : BaseItem
{

    public CombatManager manager;
    public GameObject primarySpawn;
    public GameObject secondarySpawn;
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


    //ProjectileEffect effect;
    //Sprite sprite;

    public abstract void PrimaryAbility();
    public abstract void SecondaryAbility();


    public void UsePrimaryAbility() {

        if (cooldownReady)
        {
            PrimaryAbility();
            cooldownReady = false;
            StartCoroutine(DelayCooldown(primaryAttackRate * attackRateMultiplier));
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
            StartCoroutine(DelayCooldown(secondaryAttackRate * attackRateMultiplier));
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
        attackRateMultiplier = manager.totalAttackSpeedMultiplier;
    }
    public void OnDrop()
    {
        manager = null;
        StartCoroutine(DelayPickup(1f));
    }


    public IEnumerator DelayCooldown(float attackRate)
    {
        
        cooldownReady = false;
        float timeInSeconds = 1 / attackRate;
        Debug.Log("Cooldown is: " + timeInSeconds);
        yield return new WaitForSeconds(timeInSeconds);  
        cooldownReady = true;
    }
    public IEnumerator DelayPickup(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        transform.Find("Collider").GetComponent<BoxCollider2D>().enabled = true;
        pickedUp = false;
    }
}
