using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CombatManager))]
public abstract class CharacterBehavior : Buffable
{
    
    [Header("-- MANAGER STUFF --")]
    //TODO: Class needs a major refactor into smaller, accessable classes. NOT interfaces, they don't really apply here.
    public CombatManager combatManager;
    public SpriteRenderer spriteRenderer;
    /// <summary> What this entity was spawned by. </summary>
    public EntitySpawner spawnOwner;
    [Header("Starting Weapons (if any)")]
    public GameObject startingPrimaryWeapon;
    public GameObject startingSecondaryWeapon;

    //public List<Buff> buffs;
    //public List<Debuff> debuffs;
    [Header("-- STAT CHANGES --")]
    public float baseDamageIncrease;
    public float baseProjetileSpeedIncrease; //in percent
    public float baseMoveSpeed;
    
    public float damageReductionPercent;
    public float healIncreasePercent;

    public bool canMove = true;

    //TODO: PARTIALLY ON: Refactor into Buffable class 
    [Header("-- BUFFS --")]
    public bool isInvincible;
    [Header("-- DEBUFFS --")]
    public bool isFrozen;
    public bool isPoisoned;
    public bool isSlowed;
    public bool isOnFire;
    public bool isBleeding;
    public bool isStunned;
    public bool isDrunk;
    public bool isConfused;
    public bool isRooted;

    [HideInInspector]
    public float moveSpeed;


    private void Start()
    {
        try
        {
            combatManager = gameObject.GetComponent<CombatManager>();
            CreateAndEquipStartingWeapon(true, startingPrimaryWeapon);
            CreateAndEquipStartingWeapon(false, startingSecondaryWeapon);
        }
        catch
        {
            throw new System.Exception("Entity needs combat manager!");
        }

    }

    public virtual void MoveTowards(Vector2 location)
    {
        transform.Translate(location * (moveSpeed / 100), Space.World);
    }

    public virtual void LookAtPos(Vector2 position)
    {
        Vector2 direction = position - (Vector2)transform.position;

        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public override void ApplyDamage(int damage, GameObject source = null, DamageType type = DamageType.NORMAL)
    {
        base.ApplyDamage(damage, source, type);
    }

    // =========================== UTILITY ===========================
    public void CreateAndEquipStartingWeapon(bool isPrimary, GameObject weapon)
    {
        if (weapon == null || combatManager == null) return;
        if (isPrimary)
        {
            combatManager.EquipPrimary(Instantiate(weapon));
        }
        else
        {
            combatManager.EquipSecondary(Instantiate(weapon));
        }
    }
    public void UpdateMovementStats(float updatedSpeed)
    {
        moveSpeed = updatedSpeed;
    }
    
    public int CalculateHeal(int heal)
    {
        return Mathf.FloorToInt(heal * healIncreasePercent);
    }

    public void DamageJiggle(float intensity)
    {
        //TODO: figure out how to add a jiggle on hit? OR knockback!
    }
    public IEnumerator DamageFlash(Color c)
    {
        spriteRenderer.color = c;

        yield return new WaitForSeconds(0.03f);
        spriteRenderer.color = Color.white;
    }

}
