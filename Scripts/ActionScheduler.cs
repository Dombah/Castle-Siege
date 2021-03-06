using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    IAction currentAction = null;
    
    public void StartAction(IAction action) // Cancels the previous actions
    {
        if (currentAction == action) return;
        if(currentAction != null)
        {
            print("Cancelling " + currentAction);
            currentAction.Cancel();
        }
        currentAction = action;
    }
}


