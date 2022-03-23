using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public float weapDamage;
    public float weapSpeed;
    public float weapHealth;
    public float weapKnockback;

    //ProjectileEffect effect;
    //Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        // MAKE FINAL CALCULATIONS FOR DAMAGE HERE
    }

    // Update is called once per frame
    void Update()
    {
        // Do not make anything in fixed/update unless ABSOLUTELY necessary.
    }

    private void FixedUpdate() { }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    

    public abstract void OnPrimarySlotFire();
    public abstract void OnSecondarySlotFire();


}
