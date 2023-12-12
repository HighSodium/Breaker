using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    [HideInInspector]
    public Projectile projectileScript;
    private GameObject spawnedProjectile;

    void Start()
    {
        Debug.Assert(projectileObjs.Count != 0, "Knife needs at least one projectile object in ProjectileObjs list to fire.");
    }

    public override void PrimaryAbility()
    {
        //TODO: Make swing damage
        //TODO: Apply status on third swing
        //TODO: Make animations for attack
        switch (attackPhase)
        {
            case 1:
                //TODO: make animations only play on attack instead of constantly
                Debug.Log("Swing " + attackPhase + "!");
                //anim.Play("Swing_1");
                break;
            case 2:
                Debug.Log("Swing " + attackPhase + "!");
                break;
            case 3:
                Debug.Log("Swing " + attackPhase + "!");
                break;
        }
        attackPhase++;
        if (attackPhase > attackPhaseMax) attackPhase = 1;
        
        //Debug.Log("SWING KNIFE " + attackPhase + "!  ===||HHHHHHH>");

        //attackPhase = (attackPhase % attackPhaseMax) + 1;
    }

    public override void SecondaryAbility()
    {
        Transform spawnTransform = secondarySpawn.transform; 
        spawnedProjectile = Instantiate(projectileObjs[0], spawnTransform.position, spawnTransform.rotation);

        Projectile projScript = spawnedProjectile.GetComponent<Projectile>();
        if (projScript == null) return;
        projScript.damage = secondaryDamage;
        projScript.speed = projectileSpeed;
        projScript.pierceCount = projectilePierceCount;
        projScript.lifetime = projectileLifetime;
        projScript.pierceAll = projectilePierceAll;
           


        //TODO: Update the projectile stats

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
