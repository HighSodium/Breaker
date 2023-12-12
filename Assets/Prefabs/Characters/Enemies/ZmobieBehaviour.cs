using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZmobieBehaviour : EnemyBehaviour
{
    private GameObject attackTarget;
    private new Rigidbody2D rigidbody;
    void Start()
    {
        attackTarget = GameObject.Find("Player");
        rigidbody = gameObject.GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    public override void CombatLoop()
    {   
        //transform.Translate(transform.right * (moveSpeed / 100), Space.World);
        //gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * (moveSpeed);

        rigidbody.AddForce(transform.right * (moveSpeed)*50);
        if (!IsInvoking())
        {
            InvokeRepeating("RotateToTarget", 0, 0.25f);
        }

    }
    public void RotateToTarget()
    {
        rigidbody.angularVelocity = 0;
        LookAtPos(attackTarget.transform.position);     
    }
    public override void MoveTowards(Vector2 location)
    {
        transform.Translate(location * (moveSpeed / 100), Space.World);
    }

    public override void OnDamage(GameObject source, int damage)
    {
        //TODO:Add damage sounds/effects
        //throw new System.NotImplementedException();
    }


     //Start is called before the first frame update

}
