using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearman : Enemy
{
    protected override void Attack()
    {
        animator.SetBool("Running", false);
        animator.SetBool("SetAttackingPosition", true);
        if (time_Since_Last_Attack > time_To_Wait_Untill_Next_Attack)
        {
            animator.SetTrigger("Attack");
            time_Since_Last_Attack = 0f;
        }
    }

    protected override void CallCancel()
    {
        animator.SetBool("SetAttackingPosition", false);
        StartCoroutine(WaitForAnimationToFinish());
    }

    IEnumerator WaitForAnimationToFinish()
    {
        yield return new WaitForSeconds(Mathf.Abs(animator.speed));
        base.CallCancel();
    }

}
