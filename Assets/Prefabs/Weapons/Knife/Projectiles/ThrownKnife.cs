using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownKnife : Projectile
{
    public override void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollide(Collider2D other)
    {
        //Destroy(gameObject);
        DisableAllColliders();
        ParentTo(other.gameObject.transform);
        ResetLifetime(5);

        try
        {
            Damageable script = other.gameObject.GetComponent<Damageable>();
            if (script != null) 
                script.ApplyDamage(damage, gameObject, damageType);
        }
        catch { }   
    }

    public override void OnPierce(Collider2D other)
    {
        try
        {
            other.gameObject.GetComponent<Damageable>().ApplyDamage(damage, gameObject, damageType);
        }
        catch
        {         
            //Debug.LogError("INCORRECT COLLISION WITH: " + other.transform.parent.name);
            //other.transform.root.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            //other.gameObject.transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            //transform.root.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            //Debug.Break();
            //Destroy(transform.root.gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
