using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public float baseDamageIncrease;
    public float baseProjetileSpeedIncrease; //in percent
    public int baseHealth;
    public int baseShield;
    public float baseMoveSpeed;

    public float baseArmor;

    [HideInInspector]
    public int maxHealth;

    [HideInInspector]
    public int currentHealth;

    private void Start()
    {
        maxHealth = baseHealth;
        currentHealth = maxHealth;
    }
}
