using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizzard : Enemy
{
    [SerializeField] Transform shootFrom;
    [SerializeField] Transform fireball;
    public override void OnAnimationHitEnter()
    {
        if (currentTarget != null)
            Projectiles.Create(shootFrom.position, fireball, currentTarget.transform.position, currentTarget, 50f, this); 
    }
}
