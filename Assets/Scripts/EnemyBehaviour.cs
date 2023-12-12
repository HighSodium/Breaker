using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : EntityBehavior
{

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        CombatLoop();
    }
    public abstract void CombatLoop();

    


}
