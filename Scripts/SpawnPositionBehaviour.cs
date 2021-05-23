using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositionBehaviour : MonoBehaviour
{
    public bool has_Unit = false;
    private void Update()
    {
        if(GetComponentInChildren<Ally>() == null)
        {
            has_Unit = false;
        }
        else
        {
            has_Unit = true;
        }
    }
}
