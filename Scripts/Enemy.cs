using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActionScheduler))]
public class Enemy : Character
{
    [SerializeField] int damage_To_Castle = 10;

    EnemyMovement enemyMovement;
    protected override void Awake()
    {
        base.Awake();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.OnReachedEnd += Enemy_mov_OnReachedEnd;
    }

    // Event fired in EnemyMovement.cs when the enemy reaches the end 
    private void Enemy_mov_OnReachedEnd(object sender, EnemyMovement.OnReachedEndEventArgs e)
    {
        if (e.reachedEnd) { GetComponent<HealhSystem>().Die(); }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AllyDamageable")
        {
            target = other.gameObject;
            if (attackablesInRange.Contains(target)) { return; }
            else
            {
                attackablesInRange.Add(target);
            }
        }
        if (attackablesInRange.Count >= 1)
        {
            currentTarget = attackablesInRange[0];
        }
    }

    public override void OnAnimationHitEnter()
    {
        base.OnAnimationHitEnter();
    }
    protected override void MoveToAttackRange()
    {
        GetComponent<ActionScheduler>().StartAction(this);
        transform.LookAt(currentTarget.transform);
        if (!IsInAttackRange() && currentTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, enemyMovement.step);
        }
        else if (IsInAttackRange() && currentTarget != null)
        {
            Attack();
        }
    }

    protected override void CallCancel()
    {
        enemyMovement.stop_Moving = false;
        base.CallCancel();
    }

    private void OnDestroy()
    {
        enemyMovement.OnReachedEnd -= Enemy_mov_OnReachedEnd;
    }
}   

