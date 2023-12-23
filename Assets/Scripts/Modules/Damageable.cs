using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    //typical damage
    NORMAL,
    //applies directly to health
    TRUE,
    //applies only to shield,
    EMP,
    //ignores armor
    PIERCE,

    BURN,

    COLD,

    POISON
}

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Damageable : MonoBehaviour
{
    #region FIELDS / PARAMETERS
    
    [Header("-- BASE STATS --")]
    public int baseHealth = 50;
    public int baseShield = 0;
    public int baseArmor = 0;
    public float baseDamageReduction = 0f;

    [Header("-- MAX STATS -- (dont edit)")]
    public int maxHealth;
    public int maxShield;

    [Header("-- OTHER STATS --")]
    public int extraLives = 0;
    public int armor;
    public float currentDamageReduction;

    [Header("-- HEALTH AND SHIELD --")]
    // PRIMARY SHIELD 
    [SerializeField]
    private int _health;
    public int Health
    {
        get => _health;
        set
        {
            _health = (int)Math.Clamp(value, Mathf.NegativeInfinity, maxHealth);
            if (value <= 0) OnDeath();
        }
    }

    // PRIMARY SHIELD 
    [SerializeField]
    private int _shield;
    public int Shield
    {
        get => _shield;
        set
        {
            _shield = Math.Clamp(value, 0, maxShield);
            if (value <= 0) OnShieldBreak();
        }
    }
    #endregion

    private void Start()
    {
        // this will eventually be handled by the combat manager to, like health
        Shield = baseShield;
        armor = baseArmor;
 
    }

 

    #region PUBLIC METHODS

    public virtual void ApplyDamage(int damage, GameObject source = null, DamageType type = DamageType.NORMAL)
    {
        switch (type)
        {
            // APPLYING NORMAL DAMAGE
            case DamageType.NORMAL:
                if(Shield > 0)
                {
                    int cleave = damage - Shield;
                    Shield -= damage;

                    // calculate CLEAVE after loss of shield
                    if(cleave > 0)
                        Health -= CalcTotalDamage(cleave); //apply extra damage to health        
                }
                else
                    Health -= damage;                  
                
            break;

            // APPLYING DAMAGE DIRECTLY TO HEALTH IGNORING ARMOR AND SHIELD AND REDUCTIONS
            case DamageType.TRUE:
                Health -= CalcTotalDamage(damage, false, false);
            break;

            // APPLYING DAMAGE *ONLY* TO SHIELD
            case DamageType.EMP:
                Shield -= CalcTotalDamage(damage, false);
            break;

            // APPLYING DAMAGE IGNORING ARMOR
            case DamageType.PIERCE: 
                // NOTE: probably wont use
            break;
            
            
            default:
                //Do this on all damages
            break;              
        }
    }


    /// <summary> Called when the object receives health. </summary>
    public virtual void ApplyHeal(int healAmount)
    {
        //TODO: healing
        Debug.Log("I feel healthy again! +" + healAmount);
    }

    /// <summary> Called when the object's shield reaches 0. </summary>
    public virtual void OnShieldBreak()
    {
        //TODO: shield break stuff
        Debug.Log("SHIELD BREAK!");
    }

    /// <summary> Is called when entity health reaches zero </summary>
    public virtual void OnDeath()
    {
        //TODO: lives
        if (extraLives > 0)
        {
            extraLives--;
            Health = (int)Math.Ceiling(maxHealth * 0.1f);
            Debug.Log("I LIVE! I DIE! I LIVE AGAIN!");
        }
        else
        {
            if (gameObject.TryGetComponent<CharacterBehavior>(out CharacterBehavior temp))
                if(temp.spawnOwner) temp.spawnOwner.DespawnEnemy(gameObject);
            else
                Destroy(gameObject);

        }
    }
    #endregion

    #region PRIVATE METHODS
    // ================================== PRIVATE METHODS ==================================

    /// <summary> Calculates the actual damage done after armor and reductions </summary>
    private int CalcTotalDamage(int damage, bool useArmor=true, bool useReduction=true)
    {
        int totalDamage = damage;
        if(useArmor) 
            totalDamage -= armor;
        if(useReduction) 
            totalDamage = (int)(totalDamage * (1 - currentDamageReduction)); // current armor is calculated as

        return (int)Math.Clamp(totalDamage, 1, Mathf.Infinity);
    }
    #endregion
}


