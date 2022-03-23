using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    public float moveSpeed;
    private Vector2 moveInput;
    private Vector2 mouseScreenPos;
    private Vector2 mouseWorldPos;

    private bool mouseLeft;
    private bool mouseRight;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        // movement
        
        transform.Translate(moveInput * (moveSpeed / 100), Space.World);

        //mouse aim
        mouseScreenPos = (Vector2)Mouse.current.position.ReadValue();
        mouseWorldPos = (Vector2)Camera.main.ScreenToWorldPoint(mouseScreenPos, Camera.MonoOrStereoscopicEye.Mono);
        Vector2 mouseDirection = mouseWorldPos - (Vector2)transform.position;


        float angle = Vector2.SignedAngle(Vector2.right, mouseDirection);
        transform.eulerAngles = new Vector3(0, 0, angle);
        //Vector3 lookPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
         
    }

    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    public void OnPrimaryFire(InputValue input)
    {
        mouseLeft = input.isPressed;
        
    }
    public void OnSecondaryFire(InputValue input)
    {
        mouseRight = input.isPressed;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (mouseLeft)
        {
            if (other.transform.root.CompareTag("Weapon"))
            {
                Debug.Log("YOU DID IT!");
                //AddToWeaponUI(other.transform.root);


                return;
            }
            Debug.Log("incorrect type detected -> " + other.gameObject.GetType().Name);

        }

    }
    #region STAT MODIFIERS

    public void UpdateMoveSpeed(float updatedSpeed)
    {
        moveSpeed = updatedSpeed;
    }


    #endregion
}
