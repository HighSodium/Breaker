using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{

    // TODO: abstract these to EQUIPABLE
    public GameObject startingPrimaryWeapon;
    public GameObject startingSecondaryWeapon;
    public float baseDamageIncrease;
    
    public int baseHealth;
    public int baseShield;

    //TODO: Abstract to MOVEABLE
    public float baseMoveSpeed;

    public int baseArmor;

    [HideInInspector]
    public int maxHealth;

    //[HideInInspector]
    public int currentHealth;
    public int currentShield;
    public int currentArmor;

    private void Start()
    {
        currentHealth = maxHealth = baseHealth;
        currentArmor = baseArmor;
        currentShield = baseShield;
    }
    public virtual void TakeDamage(int damage)
    {
        int totalDmg = damage - currentArmor;
        currentHealth -= totalDmg < 0 ? 0 : totalDmg;

        if (currentHealth <= 0) {
            OnDeath();
        }
    }

    /// <summary> Is called when entity health reaches zero </summary>
    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}
