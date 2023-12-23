using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerBehavior : CharacterBehavior
{
    private Vector2 moveInput;
    private Vector2 mouseWorldPos;

    private bool mouseLeft;
    private bool mouseRight;
    private bool mouseRightDown;
    private bool mouseLeftDown;
    private bool rightFireReady;
    private bool leftFireReady;

    private bool isPlayer;

    private List<Collider2D> pickupColliders;

    // Start is called before the first frame update
    void Start()
    {
        pickupColliders = new List<Collider2D>();
        combatManager = gameObject.GetComponent<CombatManager>();
        rightFireReady = leftFireReady = false;
        isPlayer = gameObject.transform.root.CompareTag("Player") ? true : false;

        WhatAmI(StatusEffect.INVINCIBLE);
    }

    private void Update()
    {

        if (mouseLeft & leftFireReady)
        {
            combatManager.FirePrimary();
        }
        if (mouseRight & rightFireReady)
        {
            combatManager.FireSecondary();
        }

        mouseWorldPos = GetMouseToWorldPos();
        LookAtPos(mouseWorldPos);
    }
    private void FixedUpdate()
    {      
        MoveTowards(moveInput);
    }

    private Vector2 GetMouseToWorldPos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(mouseScreenPos, Camera.MonoOrStereoscopicEye.Mono);
    }
    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    public void OnPrimaryFire(InputValue input)
    {      
        mouseLeft = input.isPressed;
        
        if (mouseLeft)
        {  
            GameObject itemGrab = FindClosestItem("Weapon");
            if (itemGrab != null)
            {
                leftFireReady = false;
                combatManager.EquipPrimary(itemGrab);
            }           
        }       
        else 
        {
            leftFireReady = true;
        }
    }
    public void OnSecondaryFire(InputValue input)
    {
        mouseRight = input.isPressed;

        if (mouseRight)
        {        
            GameObject itemGrab = FindClosestItem("Weapon");
            if (itemGrab != null)
            {
                rightFireReady = false;
                combatManager.EquipSecondary(itemGrab);
            }
        }
        else
        {
            rightFireReady = true;
        }
    }

    public override void ApplyDamage(int damage, GameObject source = null, DamageType type = DamageType.NORMAL)
    {
        base.ApplyDamage(damage, source, type);
        StartCoroutine(DamageFlash(Color.red));
        if (source != null) 
            Debug.Log("You took damage from " + source.name + "!");
        else
            Debug.Log("You took damage from nothing?");
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.CompareTag("Weapon"))
        {
            pickupColliders.Add(other);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.root.CompareTag("Weapon"))
        {
            pickupColliders.Remove(other);
        }
        
    }

    private GameObject FindClosestItem(string tag)
    {
        
        float minDist = float.PositiveInfinity;
        Collider2D closest = null;
        foreach (Collider2D pickup in pickupColliders)
        {         
            float tempDist = Vector2.Distance(pickup.transform.position, transform.position);
            if (tempDist < minDist)
            {
                minDist = tempDist;
                closest = pickup;
            }   
        }
        if (closest == null) return null;
        return closest.transform.root.gameObject;
    }

}
