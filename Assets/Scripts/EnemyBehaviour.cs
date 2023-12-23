using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : CharacterBehavior
{

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        CombatLoop();
    }
    public abstract void CombatLoop();

    public override void ApplyDamage(int damage, GameObject source = null, DamageType type = DamageType.NORMAL)
    {
        base.ApplyDamage(damage, source, type);
        StartCoroutine(DamageFlash(Color.red));
        Debug.Log($"{gameObject.name} took {damage} {type} damage from {source.name}!");
    }
}
