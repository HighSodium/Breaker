using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZmobieBehaviour : EnemyBehaviour
{
    private GameObject attackTarget;
    private new Rigidbody2D rigidbody;

    public float primaryCooldown = 0.5f;


    void Start()
    {
        attackTarget = GameObject.Find("Player");
        rigidbody = gameObject.GetComponent<Rigidbody2D>();


    }
    public void Attack()
    {
        if (attackTarget)
            attackTarget.GetComponent<Damageable>().ApplyDamage(1, gameObject);
    }

    // Update is called once per frame
    public override void CombatLoop()
    {
        //transform.Translate(transform.right * (moveSpeed / 100), Space.World);
        //gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * (moveSpeed);


        //MoveTowards(transform.right);
        MoveTowards(transform.right);

        if (!IsInvoking())
        {
            InvokeRepeating("RotateToTarget", 0, 0.3f);
        }

    }
    public void RotateToTarget()
    {
        rigidbody.angularVelocity = 0;
        LookAtPos(attackTarget.transform.position);
    }
    public override void MoveTowards(Vector2 location)
    {
        // dont use translate because it does not like physics interactions
        // in large groups.
        rigidbody.AddForce((moveSpeed) * 25 * location);
    }

    public override void ApplyDamage(int damage, GameObject source = null, DamageType type = DamageType.NORMAL)
    {
        base.ApplyDamage(damage, source, type);
        //TODO:Add damage sounds/effects
        //throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        if (other.gameObject == attackTarget)
        {
            InvokeRepeating("Attack", 0, primaryCooldown);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == attackTarget)
        {
            CancelInvoke("Attack");
            Debug.Log("Attacked Cancelled");
        }
    }

}