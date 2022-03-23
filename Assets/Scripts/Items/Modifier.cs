using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier : BaseItem
{
    [Tooltip("As a flat increase.")]
    public float damageIncrease = 0; // flat value increase
    [Tooltip("As a percentage multiplier.")]
    public float damageMultiplier = 0; // percentage-based increase

    [Tooltip("As a flat increase.")]
    public float projectileSpeedIncrease = 0;
    [Tooltip("As a percentage multiplier.")]
    public float projectileSpeedMulitplier = 0;

    [Tooltip("As a flat increase.")]
    public float healthIncrease = 0;
    [Tooltip("As a percentage multiplier.")]
    public float healthMultiplier = 0;

    [Tooltip("As a flat increase.")]
    public float moveSpeedIncrease = 0;
    [Tooltip("As a percentage multiplier.")]
    public float moveSpeedMultiplier = 0;

    [Tooltip("As a flat increase.")]
    public float knockbackIncrease = 0;
    [Tooltip("As a percentage multiplier.")]
    public float knockbackMulitplier = 0;


    public int stackMax;
    public bool isInWorld = true;

    private void FixedUpdate()
    {
        if (isInWorld)
        {
            transform.Rotate(transform.forward, 8);
        }
    }
    private void Start()
    {
        icon = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInWorld = false;
            other.gameObject.GetComponent<CombatManager>().AddModifier(this);
        }
    }  
}
 