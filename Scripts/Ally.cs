using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Character
{
    [SerializeField] float movementSpeed = 2f;
    [HideInInspector]
    public float step = 0f;
    protected override void Awake()
    {
        base.Awake();
        step = movementSpeed * Time.deltaTime;
    }

    protected override void MoveToAttackRange()
    {
        transform.LookAt(currentTarget.transform);
        if (!IsInAttackRange() && currentTarget != null)
        {
            animator.SetBool("Running", true);
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, step);
        }
        else if (IsInAttackRange() && currentTarget != null)
        {
            Attack();
        }
    }
}

