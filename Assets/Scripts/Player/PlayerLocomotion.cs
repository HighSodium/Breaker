using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerLocomotion : MonoBehaviour
{

    public float moveSpeed;

    private Vector2 moveInput;
    private Vector2 mouseScreenPos;
    private Vector2 mouseWorldPos;

    private bool mouseLeft;
    private bool mouseRight;
    private bool mouseRightDown;
    private bool mouseLeftDown;
    private bool readyToFire;

    private List<Collider2D> pickupColliders;
    private CombatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
        pickupColliders = new List<Collider2D>();
        combatManager = gameObject.GetComponent<CombatManager>();
        readyToFire = false;
    }

    private void FixedUpdate()
    {    
        // movement
        transform.Translate(moveInput * (moveSpeed / 100), Space.World);

        //mouse aim
        mouseScreenPos = Mouse.current.position.ReadValue();
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos, Camera.MonoOrStereoscopicEye.Mono);
        Vector2 mouseDirection = mouseWorldPos - (Vector2)transform.position;

        float angle = Vector2.SignedAngle(Vector2.right, mouseDirection);
        transform.eulerAngles = new Vector3(0, 0, angle);

        if (readyToFire)
        {
            if (mouseLeft)
            {
                combatManager.FirePrimary();
            }
            else if (mouseRight)
            {
                combatManager.FireSecondary();
            }
        }

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
                readyToFire = false;
                combatManager.EquipPrimary(itemGrab);
            }           
        }
        
        else if (!mouseLeft)
        {
            readyToFire = true;
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
                readyToFire = false;
                combatManager.EquipSecondary(itemGrab);
            }
        }

        if (!mouseRight)
        {
            readyToFire = true;
        }
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
        float minDist = 999;
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

    #region STAT MODIFIERS

    public void UpdateMoveSpeed(float updatedSpeed)
    {
        moveSpeed = updatedSpeed;
    }


    #endregion
}
