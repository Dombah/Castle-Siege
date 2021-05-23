using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    public void Hit()
    {
        GetComponentInParent<Character>().OnAnimationHitEnter();
    }
}
