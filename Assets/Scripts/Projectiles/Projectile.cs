using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float baseDamage;
    float baseSpeed;
    float baseHealth;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake(){}
    // Update is called once per frame
    void Update() {}

    private void FixedUpdate()
    {

       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if(other.gameObject.CompareTag("Enemy"){
        //  other.gameObject.ApplyDamage;
        //}


        if (!other.gameObject.CompareTag("Player")) 
        {
            Destroy(gameObject);

            
        }
    }
}
