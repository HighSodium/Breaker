using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class CharacterLocomotion : MonoBehaviour
{

    public float moveSpeed;

    private Vector2 moveInput;
    private Vector2 mouseWorldPos;

    private bool mouseLeft;
    private bool mouseRight;
    private bool mouseRightDown;
    private bool mouseLeftDown;
    private bool readyToFire;

    private bool isPlayer;

    private List<Collider2D> pickupColliders;
    private CombatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
        pickupColliders = new List<Collider2D>();
        combatManager = gameObject.GetComponent<CombatManager>();
        readyToFire = false;
        isPlayer = gameObject.transform.root.CompareTag("Player") ? true : false;

    }

    private void FixedUpdate()
    {
        // movement
        
        //transform.Translate(moveInput * (moveSpeed / 100), Space.World);

        //mouse aim
        if (isPlayer)
        {
            MoveTowards(moveInput);

            mouseWorldPos = GetMouseToWorldPos();
            LookAtPos(mouseWorldPos);
        }
        else
        {

        }
        
        //Vector2 mouseDirection = mouseWorldPos - (Vector2)transform.position;

        //float angle = Vector2.SignedAngle(Vector2.right, mouseDirection);
        //transform.eulerAngles = new Vector3(0, 0, angle);

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
    
    public void MoveTowards(Vector2 location)
    {
        transform.Translate(location * (moveSpeed / 100), Space.World);
    }

    private Vector2 GetMouseToWorldPos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(mouseScreenPos, Camera.MonoOrStereoscopicEye.Mono);
    }
    private void LookAtPos(Vector2 position)
    {
        Vector2 mouseDirection = position - (Vector2)transform.position;

        float angle = Vector2.SignedAngle(Vector2.right, mouseDirection);
        transform.eulerAngles = new Vector3(0, 0, angle);
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

    public void UpdateMovementStats(float updatedSpeed)
    {
        moveSpeed = updatedSpeed;
    }


    #endregion
}
