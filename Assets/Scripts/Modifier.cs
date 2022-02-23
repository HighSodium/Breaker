using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier : MonoBehaviour
{
    public string modName = "Default Name";
    public string description = "...Information Withheld...";
    
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

    public Sprite icon;
    protected AudioClip pickupSound;

    private void Start()
    {
        icon = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CombatManager>().AddModifier(this);
            Destroy(gameObject);
        }
    }
}
