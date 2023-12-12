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
    EntityBehavior entityStats;

    public GameObject primaryWeapon;
    public Weapon primaryWeaponScript;
    public GameObject primaryWeaponSocket;
    public GameObject primaryProjectileSpawn;

    public GameObject secondaryWeapon;
    public Weapon secondaryWeaponScript;
    public GameObject secondaryWeaponSocket;    
    public GameObject secondaryProjectileSpawn;

    [Header("Damage")]
    public float totalDamageIncrease = 0; // flat value increase
    public float totalDamageMultiplier = 1; // percentage-based increase
    public float totalDamage;
    [Header("Projectiles")]
    public float totalProjectileSpeedIncrease = 0;
    public float totalProjectileSpeedMulitplier = 1;
    public float totalProjectileSpeed;
    [Header("Attack Speed")]
    public float totalAttackSpeedIncrease = 0;
    public float totalAttackSpeedMultiplier = 1;
    public float totalAttackSpeed;
    [Header("Health")]
    public float totalHealthIncrease = 0;
    public float totalHealthMuliplier = 1;
    public float totalHealth;
    [Header("Move Speed")]
    public float totalMoveSpeedIncrease = 0;
    public float totalMoveSpeedMultiplier = 1;
    public float totalMoveSpeed;
    [Header("Knockback")]
    public float totalKnockbackIncrease = 0;
    public float totalKnockbackMulitplier = 1;
    public float totalKnockback;
    [Header("Other Params")]
    public bool useBothAbilities;

    float totalProjectileAmount;


    void Start()
    {
        modList = new List<Modifier>();
        //playerStats = (EntityBehavior)gameObject.GetComponent("EntityBehavior");
        entityStats = gameObject.GetComponent<EntityBehavior>();
        GetPlayerStatValues(entityStats);// get base stats from player

        RecalculateStats(); //update totals

        UpdatePlayerHealth(); // make starting health
        UpdatePlayerMoveSpeed(); // make starting speed

        // check to see if the using character has a starting weapon

        if (entityStats.startingPrimaryWeapon != null) EquipPrimary(Instantiate(entityStats.startingPrimaryWeapon));
        if (entityStats.startingSecondaryWeapon != null) EquipSecondary(Instantiate(entityStats.startingSecondaryWeapon));

    }

    public void FirePrimary()
    {
        // do things that need to be done before firing;
        if (primaryWeapon != null)
        {
            primaryWeaponScript.UsePrimaryAbility();
            if(useBothAbilities) primaryWeaponScript.UseSecondaryAbility();
        }
    }
    public void FireSecondary()
    {
        // do things that need to be done before firing;
        if (secondaryWeapon != null)
        {
            secondaryWeaponScript.UseSecondaryAbility();
            if(useBothAbilities) secondaryWeaponScript.UsePrimaryAbility();
        }
    }
    public GameObject EquipPrimary(GameObject weapon)
    {
        AttachToWeaponSlot(true, weapon);     
        //update UI
        //recalculate stats
        return weapon;
    }
    public GameObject EquipSecondary(GameObject weapon)
    {
        
        AttachToWeaponSlot(false, weapon);
        return weapon;
    }
    public void SwapWeapons()
    {
        GameObject tempObj = primaryWeapon;
        primaryWeapon = secondaryWeapon;
        secondaryWeapon = tempObj;

        primaryWeaponScript = (Weapon)primaryWeapon.GetComponent("Weapon");
        secondaryWeaponScript = (Weapon)secondaryWeapon.GetComponent("Weapon");

        //UpdateWeaponUI();
        //RecalculateWeaponStats();
    }
    public Modifier AddModifier(Modifier mod)
    {
        modList.Add(mod); 
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

        totalAttackSpeedIncrease += mod.attackSpeedIncrease;
        totalAttackSpeedMultiplier += mod.attackSpeedMultiplyer;

        RecalculateStats();
        UpdatePlayerHealth();
        UpdatePlayerMoveSpeed();
    }
    private void SubtractModFromStats (Modifier mod)
    {
        totalDamageIncrease -= mod.damageIncrease; // flat value increase
        totalDamageMultiplier -= mod.damageMultiplier; // percentage-based increase

        totalProjectileSpeedIncrease -= mod.projectileSpeedIncrease;
        totalProjectileSpeedMulitplier -= mod.projectileSpeedMulitplier;

        totalHealthIncrease -= mod.healthIncrease;
        totalHealthMuliplier -= mod.healthMultiplier;

        totalMoveSpeedIncrease -= mod.moveSpeedIncrease;
        totalMoveSpeedMultiplier -= mod.moveSpeedMultiplier;

        totalKnockbackIncrease -= mod.knockbackIncrease;
        totalKnockbackMulitplier -= mod.knockbackMulitplier;

        RecalculateStats();
        UpdatePlayerHealth();
        UpdatePlayerMoveSpeed();
    }
    public void RecalculateStats()
    {
        totalDamage             = totalDamageIncrease           * totalDamageMultiplier;    
        totalHealth             = totalHealthIncrease           * totalHealthMuliplier;
        totalMoveSpeed          = totalMoveSpeedIncrease        * totalMoveSpeedMultiplier;
        totalKnockback          = totalKnockbackIncrease        * totalKnockbackMulitplier;
        totalAttackSpeed        = totalAttackSpeedIncrease      * totalAttackSpeedMultiplier;
        totalProjectileSpeed    = totalProjectileSpeedIncrease  * totalProjectileSpeedMulitplier;
    }
    private void GetPlayerStatValues(EntityBehavior stats)
    {
        totalDamageIncrease += stats.baseDamageIncrease;
        totalHealthIncrease += stats.baseHealth;
        totalMoveSpeedIncrease += stats.baseMoveSpeed;
        totalProjectileSpeedIncrease += stats.baseProjetileSpeedIncrease;     
    }
    private void UpdatePlayerHealth()
    {
        EntityBehavior playerInfo = (EntityBehavior)gameObject.GetComponent("EntityBehavior");

        playerInfo.health += Mathf.RoundToInt((totalHealth - playerInfo.maxHealth));
        playerInfo.maxHealth = Mathf.RoundToInt(totalHealth);

    }
    private void UpdatePlayerMoveSpeed()
    {
        entityStats.UpdateMovementStats(totalMoveSpeed);
    }
    private void AttachToWeaponSlot(bool isPrimary, GameObject weapon)
    {
        if (weapon == null) return;
        EjectWeaponSlot(isPrimary); //get rid of old weapons first

        Transform weaponTrans = weapon.transform;
        Weapon weaponScript = (Weapon)weapon.GetComponent("Weapon");  

        if (weaponScript != null)
        {
            weaponScript.manager = this;
            weaponTrans.Find("Collider").gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //find the primary position for parenting

            Transform parentTrans = isPrimary ? primaryWeaponSocket.transform : secondaryWeaponSocket.transform;
            weaponTrans.position = parentTrans.position;
            weaponTrans.rotation = parentTrans.rotation;
            weaponTrans.parent = parentTrans;
            if (isPrimary)
            {
                FlipWeapon(weapon);
                primaryWeapon = weapon;
                primaryWeaponScript = weaponScript;
            }
            else
            {
                secondaryWeapon = weapon;
                secondaryWeaponScript = weaponScript;
            }
            // make the weapon run its checks
            weaponScript.OnPickup();

        }  
    }
    private void EjectWeaponSlot(bool isPrimary)
    {
        if (isPrimary)
        {
            if (!primaryWeapon) return;
            FlipWeapon(primaryWeapon);
            primaryWeapon.transform.position = GetRandomPosAroundPlayer(1.5f);
            primaryWeaponScript.OnDrop();
            primaryWeapon = null;
            primaryWeaponScript = null;      
        }
        else
        {        
            if (!secondaryWeapon) return;           
            secondaryWeapon.transform.position = GetRandomPosAroundPlayer(1.5f);
            secondaryWeaponScript.OnDrop();
            secondaryWeapon = null;
            secondaryWeaponScript = null;
        }   
    }
    public Vector2 GetRandomPosAroundPlayer(float radius)
    {
        float rads = UnityEngine.Random.Range(0, 2*MathF.PI);
        float x = MathF.Cos(rads);
        float y = MathF.Sin(rads);
        return (Vector2)transform.position + new Vector2(x, y) * radius;
    }
    private void FlipWeapon(GameObject weapon)
    {
        weapon.transform.localScale = Vector3.Scale(weapon.transform.localScale, new Vector3(1, -1, 1));
    }



}
