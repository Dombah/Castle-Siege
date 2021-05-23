using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizzardTower : Tower
{
    [SerializeField] protected Transform objectToMove;
    [SerializeField] protected Transform weaponToMove;

    [SerializeField] Transform shootFrom = null;
    [SerializeField] Transform projectileType = null;

    [SerializeField] protected float time_To_Wait_For_Next_Attack = 2f;
    private float time_Since_Last_Attack = Mathf.Infinity;

    protected override void Update()
    {
        time_Since_Last_Attack += Time.deltaTime;
        base.Update();
        if (targetEnemy && IsEnemyInRange())
        {
            objectToMove.LookAt(2 * objectToMove.transform.position - targetEnemy.transform.Find("LookAt Target").position);
            weaponToMove.LookAt(2 * objectToMove.transform.position - targetEnemy.transform.position);
            ProcessFiring();
        }
    }

    protected virtual void ProcessFiring()
    {
        if (time_Since_Last_Attack > time_To_Wait_For_Next_Attack)
        {
            Projectiles.Create(shootFrom.position, projectileType, targetEnemy.position, targetEnemy.gameObject, 50f, this);
            time_Since_Last_Attack = 0f;
        }
    }
}
