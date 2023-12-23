using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class Weapon : BaseItem
{

    public CombatManager manager;
    public GameObject primarySpawn;
    public GameObject secondarySpawn;
    

    //public Animation primaryAnimation;
    //public Animation secondaryAnimation;
    // stats
    [Header("Weapon Info")]
    //public AnimationClip primaryAnimation;
    //public AnimationClip secondaryAnimation;
    public AnimatorController primaryAnimationController;
    public AnimatorController secondaryAnimationController;

    [Header("Projectile Stats")]
    public List<GameObject> projectileObjs;
    public int projectileHealth;
    public float projectileLifetime;
    public float projectileSpeed;
    public int projectileDamage;
    public int projectilePierceCount;
    public bool projectilePierceAll;
    [Header("Damage Stats")]
    public DamageType primaryDamageType;
    public DamageType secondaryDamageType;
    [Space]
    public int primaryDamage;
    public int secondaryDamage;
    [Space]
    [Tooltip ("Attacks per second (1/DPS)")]
    public float primaryAttacksPerSec;
    [Tooltip("Attacks per second (1/DPS)")]
    public float secondaryAttacksPerSec;
    [Space]
    public float attackRateMultiplier = 1;
    [Space]
    public float knockback;
    // functionality
    private bool primaryCooldownReady = true;
    private bool secondaryCooldownReady = true;
    public bool isPrimary;

    public bool canDamage = true;
    public int attackPhase = 0;
    public int attackPhaseMax;


    //ProjectileEffect effect;
    //Sprite sprite;

    public abstract void PrimaryAbility();
    public abstract void SecondaryAbility();

    private void Start()
    {
        Debug.Assert(attackPhaseMax != 0, itemName +" needs a number of primary attack phases. Set in prefab.");
    }
    public void UsePrimaryAbility() {

        if (primaryCooldownReady)
        {
            PrimaryAbility();
            StartCoroutine(StartPrimaryCooldown(primaryAttacksPerSec));
        }
        else
        {
            // do cooldown stuff
        }

    }
    public void UseSecondaryAbility()
    {
        if (secondaryCooldownReady)
        {
            SecondaryAbility();          
            StartCoroutine(StartSecondaryCooldown(secondaryAttacksPerSec));
        }
        else
        {
            //do cooldown stuff
        }
    }
    public void OnPickup()
    {
        pickedUp = true;
        UpdateAttackSpeed();
        
    }
    public void OnDrop()
    {
        transform.parent = null;
        manager = null;
        StartCoroutine(DelayEnable(1f));
    }

    public void ApplyDamage(float damage)
    {
        
    }
    public float UpdateAttackSpeed()
    {
        return attackRateMultiplier = manager.totalAttackSpeedMultiplier;
    }
    public IEnumerator StartPrimaryCooldown(float attackRate)
    {
        UpdateAttackSpeed();
        primaryCooldownReady = false;
        float timeInSeconds = 1 / (attackRate * attackRateMultiplier);
        yield return new WaitForSeconds(timeInSeconds);
        primaryCooldownReady = true;
    }
    public IEnumerator StartSecondaryCooldown(float attackRate)
    {
        UpdateAttackSpeed();
        secondaryCooldownReady = false;
        float timeInSeconds = 1 / (attackRate*attackRateMultiplier);
        yield return new WaitForSeconds(timeInSeconds);  
        secondaryCooldownReady = true;
    }
    public IEnumerator DelayEnable(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        transform.Find("Collider").GetComponent<BoxCollider2D>().enabled = true;
        pickedUp = false;
    }
}
