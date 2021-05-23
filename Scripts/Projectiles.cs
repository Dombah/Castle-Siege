using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectiles : MonoBehaviour
{
    protected MonoBehaviour caller; // Object from which the projectile is caller from

    protected bool has_Hit = false; // Stops OnTriggerEnter executing more than once per hit
    public static void Create(Vector3 spawnPos, Transform projectileType, Vector3 targetPosition, GameObject currentTarget, float speed, MonoBehaviour caller)
    {
        Transform projectileObject = Instantiate(projectileType, spawnPos, Quaternion.identity); // Spawn at location
        Projectiles projectileBehaviour = projectileObject.GetComponent<Projectiles>(); 
        projectileBehaviour.Setup(targetPosition, spawnPos, currentTarget, speed, caller);
    }

    private Vector3 targetPosition;
    private Vector3 spawnPos;
    // Set values
    private void Setup(Vector3 targetPosition, Vector3 spawnPos, GameObject currentTarget, float speed, MonoBehaviour caller)
    {
        this.targetPosition = targetPosition;
        this.spawnPos = spawnPos;
        this.currentTarget = currentTarget;
        this.speed = speed;
        this.caller = caller;
    }

    private float speed;
    private void Update() // Sets the correct shooting angle depending on target
    {
        Vector3 moveDir = (targetPosition - spawnPos).normalized;    
        transform.position += moveDir * speed * Time.deltaTime;

        float angles = GetAngleFromVector(moveDir);
        transform.eulerAngles = new Vector3(angles, angles, 90f);
    }

    private float GetAngleFromVector(Vector3 dir) // Angle calculating formula
    {
        dir = dir.normalized;
        float f = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (f < 0) f += 360f;
        return f;
    }


    protected GameObject currentTarget;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyDamageable")
        {
            if (has_Hit) return;
            if(caller.GetComponent<Tower>() != null || caller.GetComponent<Ally>() != null) // If its not frendly fire proceed
            if(other.gameObject == currentTarget.gameObject)
            {
                GameObject hit = other.gameObject; // Reference to hit object
                hit.GetComponent<HealhSystem>().TakeDamage(1); 
                has_Hit = true;
                Destroy(gameObject);
            }
        }
        else if(other.gameObject.tag == "AllyDamageable")
        {
            if (has_Hit) return;
            if(caller.GetComponent<Tower>() == null) // If its not frendly fire proceed
            {
                if (other.gameObject == currentTarget.gameObject)
                {
                    GameObject hit = other.gameObject; // Reference to hit object
                    hit.GetComponent<HealhSystem>().TakeDamage(1);
                    has_Hit = true;
                    Destroy(gameObject);
                }
            }
        }
        else // If the projectile hits something with trigger enabled other than damageable destroy projectile after 2 seconds
        {
            Destroy(gameObject, 2f);
        }
    }
}
