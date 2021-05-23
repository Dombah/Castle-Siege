using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    [SerializeField] Transform shootFrom;
    [SerializeField] Transform arrow;

    protected override void Attack()
    {
        base.Attack();
    }
    protected override void Update()
    {
        base.Update();
        //print(currentTarget);
    }

    public override void OnAnimationHitEnter()
    {
        if (currentTarget != null)
            Projectiles.Create(shootFrom.position, arrow, currentTarget.transform.position, currentTarget, 50f, this);
    }

}
