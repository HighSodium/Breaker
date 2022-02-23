using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Start is called before the first frame update

    //List<PlayerMod> playerMods;
    //List<ProjectileMod> projectileMods;
    public List<Modifier> modList;
    PlayerInfo playerStats;
    

    public float totalDamageIncrease = 0; // flat value increase
    public float totalDamageMultiplier = 1; // percentage-based increase
    public float totalDamage;

    public float totalProjectileSpeedIncrease = 0;
    public float totalProjectileSpeedMulitplier = 1;
    public float totalProjectileSpeed;

    public float totalHealthIncrease = 0;
    public float totalHealthMuliplier = 1;
    public float totalHealth;

    public float totalMoveSpeedIncrease = 0;
    public float totalMoveSpeedMultiplier = 1;
    public float totalMoveSpeed;

    public float totalKnockbackIncrease = 0;
    public float totalKnockbackMulitplier = 1;
    public float totalKnockback;

    float totalProjectileAmount;

    void Start()
    {
        modList = new List<Modifier>();
        playerStats = gameObject.GetComponent<PlayerInfo>();
        GetPlayerStatValues(playerStats);// get base stats from player

        RecalculateStats(); //update totals

        UpdatePlayerHealth(); // make starting health
        UpdatePlayerMoveSpeed(); // make starting speed
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Modifier AddModifier(Modifier mod)
    {
        modList.Add(mod); //TODO: MOD IS BEING DESTROYED AND IS NOT IN LIST!!!!!!

        AddStatsFromMod(mod);
        return mod;
    }

    private void AddStatsFromMod(Modifier mod)
    {
        totalDamageIncrease += mod.damageIncrease; // flat value increase
        totalDamageMultiplier += mod.damageMultiplier; // percentage-based increase
        
        totalProjectileSpeedIncrease += mod.projectileSpeedIncrease;
        totalProjectileSpeedMulitplier += mod.projectileSpeedMulitplier;
        
        totalHealthIncrease += mod.healthIncrease;
        totalHealthMuliplier += mod.healthMultiplier;
        
        totalMoveSpeedIncrease += mod.moveSpeedIncrease;
        totalMoveSpeedMultiplier += mod.moveSpeedMultiplier;
        
        totalKnockbackIncrease += mod.knockbackIncrease;
        totalKnockbackMulitplier += mod.knockbackMulitplier;

        RecalculateStats();
        UpdatePlayerHealth();
        UpdatePlayerMoveSpeed();
    }

    void RecalculateStats()
    {
        totalDamage = totalDamageIncrease * totalDamageMultiplier;
        totalProjectileSpeed = totalProjectileSpeedIncrease * totalProjectileSpeedMulitplier;
        totalHealth = totalHealthIncrease * totalHealthMuliplier;
        totalMoveSpeed = totalMoveSpeedIncrease * totalMoveSpeedMultiplier;
        totalKnockback = totalKnockbackIncrease * totalKnockbackMulitplier;
    }

   

    private void GetPlayerStatValues(PlayerInfo stats)
    {
        totalDamageIncrease += stats.baseDamageIncrease;
        totalHealthIncrease += stats.baseHealth;
        totalMoveSpeedIncrease += stats.baseMoveSpeed;
        totalProjectileSpeedIncrease += stats.baseProjetileSpeedIncrease;     
    }

    private void UpdatePlayerHealth()
    {
        PlayerInfo playerInfo = gameObject.GetComponent<PlayerInfo>();

        playerInfo.currentHealth += Mathf.RoundToInt((totalHealth - playerInfo.maxHealth));
        playerInfo.maxHealth = Mathf.RoundToInt(totalHealth);

    }

    private void UpdatePlayerMoveSpeed()
    {
        gameObject.GetComponent<PlayerMovement>().moveSpeed = totalMoveSpeed;
    }
}
