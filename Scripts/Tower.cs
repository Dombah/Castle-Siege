using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float attackRange = 20;

    protected Transform targetEnemy;
    public Waypoint baseWaypoint; // the waypoint the tower is standing on

    protected virtual void  Update()
    {
        SetTargetEnemy();
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<Enemy>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (Enemy testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy, testEnemy);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform closestEnemy, Enemy testEnemy)
    {
        float distanceFromClosest = Vector3.Distance(closestEnemy.transform.position, gameObject.transform.position);
        float distanceFromTest = Vector3.Distance(testEnemy.transform.position, gameObject.transform.position);
        if (distanceFromTest == 0 || distanceFromClosest == 0) { Debug.LogError("Error. Turret can't be placed on the enemy path."); Debug.Break(); }
        if (distanceFromClosest < distanceFromTest)
        {
            return closestEnemy;
        }
        else
        {
            return testEnemy.transform;
        }
    }

    protected virtual bool IsEnemyInRange()
    {
        if (targetEnemy == null) return false;
        float distance = Vector3.Distance(targetEnemy.position, transform.position);
        if (distance <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
