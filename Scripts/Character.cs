using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealhSystem))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(ActionScheduler))]
public class Character : MonoBehaviour, IAction
{
    // Damage 
    [SerializeField] int damage_To_Knights = 10;
    [SerializeField] int damage_To_Archers = 10;
    [SerializeField] int damage_To_Spearmen = 10;

    // Range
    [SerializeField] float attack_Range = 10f; // Range from where the enemy can attack the target
    [SerializeField] float sight_Range = 15f; // Range in which the enemy can see the target

    [SerializeField] protected float time_To_Wait_Untill_Next_Attack = 3f;

    SphereCollider sight_Radius;

    public List<GameObject> attackablesInRange = new List<GameObject>();

    // Attackable target
    public GameObject target;
    public GameObject currentTarget = null;

    // Visual indicators
    protected GameObject attack_Range_Indicator;
    protected GameObject sight_Range_Indicator;

    protected float time_Since_Last_Attack = Mathf.Infinity;


    protected AudioSource myAudioSource;
    protected HealhSystem healhSystem;
    protected Animator animator;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        healhSystem = GetComponent<HealhSystem>();

        SetIndicators();
        SetRangeIndicators();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyDamageable")
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

    // Sets the radius at which the enemy can see the target and the visual 
    // indicators of the enemy attack range and sight range
    protected virtual void SetRangeIndicators()
    {
        sight_Radius = GetComponent<SphereCollider>();
        sight_Radius.radius = sight_Range / 1.5f;

        Vector3 attack_value = new Vector3(attack_Range, 0.0001f, attack_Range);
        attack_Range_Indicator.transform.localScale = attack_value;

        Vector3 sight_value = new Vector3(sight_Range, 0.0001f, sight_Range);
        sight_Range_Indicator.transform.localScale = sight_value;
    }

    // Gets the indicator GameObject type from the child
    private void SetIndicators()
    {
        attack_Range_Indicator = transform.Find("Attack Range").gameObject;
        sight_Range_Indicator = transform.Find("Sight Range").gameObject;
    }

    protected virtual void Update()
    {
        time_Since_Last_Attack += Time.deltaTime;
        if (IsInMovingRange())
        {
            MoveToAttackRange();
        }
    }

    protected virtual void MoveToAttackRange()
    {
        transform.LookAt(currentTarget.transform);
        if (!IsInAttackRange() && currentTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, 2f * Time.deltaTime); 
        }
        else if (IsInAttackRange() && currentTarget != null)
        {
            GetComponent<SphereCollider>().enabled = false;
            Attack();
        }
    }

    private void OnDrawGizmos()
    {
        if (currentTarget != null)
            Debug.DrawLine(transform.position, currentTarget.transform.position);
    }

    protected virtual void Attack()
    {
        animator.SetBool("Running", false);
        if (time_Since_Last_Attack > time_To_Wait_Untill_Next_Attack)
        {
            ProcessTargetValidity();
            animator.SetTrigger("Attack");
            time_Since_Last_Attack = 0f;
        }
    }

    protected virtual bool IsInMovingRange()
    {
        ProcessTargetValidity();
        if (currentTarget == null) return false;
        return Vector3.Distance(transform.position, currentTarget.transform.position) <= sight_Range;
    }

    protected virtual bool IsInAttackRange()
    {
        if (currentTarget == null) return false;
        return Vector3.Distance(transform.position, currentTarget.transform.position) <= attack_Range;
    }

    private void ProcessTargetValidity()
    {
        if (attackablesInRange.Count != 0 && attackablesInRange[0] == null)
        {
            attackablesInRange.RemoveAt(0);
            if (attackablesInRange.Count != 0)
            {
                currentTarget = attackablesInRange[0];
                if(!IsInMovingRange())
                {
                    CallCancel();
                }
            }
            else
            {
                GetComponent<SphereCollider>().enabled = true;
                CallCancel();
                currentTarget = null;
            }
            time_Since_Last_Attack = 0;
        }
    }
    protected virtual void CallCancel()
    {
        Cancel();
    }
    public void Cancel()
    {
        animator.SetBool("Running", true);
        animator.ResetTrigger("Attack");

    }
    // Called from AttackDetection.cs
    public virtual void OnAnimationHitEnter()
    {
        currentTarget.GetComponent<HealhSystem>().TakeDamage(1);
    }
}
