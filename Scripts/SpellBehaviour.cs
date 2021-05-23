using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : Projectiles
{
    [SerializeField] ParticleSystem explosion;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyDamageable")
        {
            if (has_Hit) return;
            if (caller.GetComponent<Tower>() != null || caller.GetComponent<Ally>() != null)
                if (other.gameObject == currentTarget.gameObject)
                {
                    GameObject hit = other.gameObject;
                    hit.GetComponent<HealhSystem>().TakeDamage(1);
                    has_Hit = true;
                    Destroy(gameObject);
                }
        }
        else if (other.gameObject.tag == "AllyDamageable")
        {
            if (has_Hit) return;
            if (caller.GetComponent<Tower>() == null)
            {
                if (other.gameObject == currentTarget.gameObject)
                {
                    GameObject hit = other.gameObject;
                    hit.GetComponent<HealhSystem>().TakeDamage(1);
                    has_Hit = true;
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Destroy(gameObject, 2f);
        }
    }
}
