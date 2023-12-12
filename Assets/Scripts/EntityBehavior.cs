using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehavior : MonoBehaviour
{
    //TODO: REFACTOR THIS WHOLE DAMN FUCKING CLASS... somehow...
    public CombatManager combatManager;
    public SpriteRenderer spriteRenderer;
    /// <summary> What this entity was spawned by. </summary>
    public EntitySpawner spawnOwner;
    public GameObject startingPrimaryWeapon;
    public GameObject startingSecondaryWeapon;

    //public List<Buff> buffs;
    //public List<Debuff> debuffs;

    public float baseDamageIncrease;
    public float baseProjetileSpeedIncrease; //in percent
    public int baseHealth;
    public int baseShield;
    public int baseArmor;
    public float baseMoveSpeed;
    
    public float damageReductionPercent;
    public float healIncreasePercent;

    public bool canMove = true;

    //STATUSES
    public bool isInvincible;
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
    public int maxHealth;

    //[HideInInspector]
    public int health;
    [HideInInspector]
    public float moveSpeed;

    private void Start()
    {
        health = maxHealth = baseHealth;
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

    //TODO: Take these out and replace with DAMAGEABLE only when needed
    /// <summary> Is called when entity takes damage from ApplyDamage </summary>
    public abstract void OnDamage(GameObject source, int damage);
    /// <summary> Is called when entity health reaches zero </summary>
    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }

    public abstract void MoveTowards(Vector2 location);

    public virtual void LookAtPos(Vector2 position)
    {
        Vector2 direction = position - (Vector2)transform.position;

        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public virtual void TakeDamage(int damage, GameObject damageSource)
    {
        //Debug.Break();
        //health -= CalculateDamage(damage);
        StartCoroutine(DamageFlash(Color.red));
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
            if(spawnOwner) spawnOwner.DespawnEnemy(gameObject);
            return;
        }
        OnDamage(damageSource, damage);
    }
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
    public int CalculateDamage(int damage)
    {
        //float mitigation = baseArmor * damageReductionPercent;
        return Mathf.FloorToInt((damage - baseArmor) * damageReductionPercent);
    }
    public int CalculateHeal(int heal)
    {
        return Mathf.FloorToInt(heal * healIncreasePercent);
    }

    public void DamageJiggle(float intensity)
    {
        //TODO: figure out how to add a jiggle on hit? maybe? somehow...
    }
    public IEnumerator DamageFlash(Color c)
    {
        spriteRenderer.color = c;
        yield return new WaitForSeconds(0.03f);
        spriteRenderer.color = Color.white;
    }

}
