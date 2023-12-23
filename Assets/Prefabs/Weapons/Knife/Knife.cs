using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    [HideInInspector]
    public Projectile projectileScript;
    private GameObject spawnedProjectile;

    [SerializeField]
    protected int _knifeFans = 0;
    public int KnifeFans { get => _knifeFans; set => Mathf.Clamp(value, 0, 18); }

    [SerializeField]
    protected float _knifeFanDegrees = 20f;
    public float KnifeFanDegrees { get => _knifeFanDegrees; set => Mathf.Clamp(value, 1, 180); }

    void Start()
    {
        Debug.Assert(projectileObjs.Count != 0, "Knife needs at least one projectile object in ProjectileObjs list to fire.");
    }

    public override void PrimaryAbility()
    {
        //TODO: Make swing damage
        //TODO: Apply status on third swing
        //TODO: Make animations for attack
        //TODO: CURRENTLY PROCRASTINATING: Playing animation from character socket for each swing
                //referenced by CombatManager and passed here.
                //Animation is not playing correctly. Rotations are wrong when not swinging.
                //Animation Controller is not passing to Animator on socket correctly.
        switch (attackPhase)
        {
            case 1:
                //TODO: make animations only play on attack instead of constantly
                Debug.Log("Swing " + attackPhase + "!");
                manager.primaryAnimator.Play("KnifePrimary1");
                break;
            case 2:
                Debug.Log("Swing " + attackPhase + "!");
                //manager.primaryAnimation.Play();
                break;
            case 3:
                Debug.Log("Swing " + attackPhase + "!");
                //manager.primaryAnimation.Play();
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

        for (int i = -KnifeFans; i <= KnifeFans; i++)
        {
            spawnedProjectile = Instantiate(projectileObjs[0], spawnTransform.position, spawnTransform.rotation);
            spawnedProjectile.transform.Rotate(transform.forward, KnifeFanDegrees * i);

            if (spawnedProjectile == null) return;
            Projectile projScript = spawnedProjectile.GetComponent<Projectile>();

            if (projScript == null) return;
            projScript.damage = projectileDamage;
            projScript.speed = projectileSpeed;
            projScript.pierceCount = projectilePierceCount;
            projScript.lifetime = projectileLifetime;
            projScript.pierceAll = projectilePierceAll;
            projScript.damageType = secondaryDamageType;
        }

        //TODO: Update the projectile stats

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
