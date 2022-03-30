using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public GameObject projectileObj;
    public Projectile projectileScript;

    private GameObject previousProjectile;



    // Start is called before the first frame update
    void Start()
    {
        projectileScript = projectileObj.GetComponent<Projectile>();
    }

    public override void PrimaryAbility()
    {
        Debug.Log("SWING KNIFE!  ||=HHHHHHHHHH>");
    }

    public override void SecondaryAbility()
    {
        if (projectileObj != null)
        {
            Debug.Log("FIRE KNIFE!  ||=BBBBBBBBBB>");
            Transform spawnTransform = secondarySpawn.transform;     
            previousProjectile = Instantiate(projectileObj, spawnTransform.position, spawnTransform.rotation);
            Debug.Log(spawnTransform.rotation);
            Debug.Log(previousProjectile.transform.rotation);

            //TODO: Update the projectile stats
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
