using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IAction
{
    [Tooltip("In seconds")] [SerializeField] float movingSpeed = 2f;
    [Tooltip("0 for ground")] [SerializeField] float enemySpawnHeight = 5f;
    [SerializeField] ParticleSystem reachedEndParticles;
    List<Waypoint> path = new List<Waypoint>();

    [HideInInspector]
    public float step;

    public event EventHandler<OnReachedEndEventArgs> OnReachedEnd;
    public class OnReachedEndEventArgs : EventArgs
    {
        public bool reachedEnd;
    }

    Waypoint waypoint;
    int waypointPosition = 0;

    public bool stop_Moving = false;

    void Start()
    {
        step = movingSpeed * Time.deltaTime;
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.GetPath();
        waypoint = path[waypointPosition];
    }

    private void Update()
    {
        if(!stop_Moving)
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        GetComponent<ActionScheduler>().StartAction(this); // Cancel enemy.cs while moving
        Vector3 targetPosition = new Vector3(waypoint.transform.position.x, enemySpawnHeight, waypoint.transform.position.z);
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            transform.LookAt(targetPosition);
        }
        else
        {
            if (waypointPosition < path.Count - 1)
            {
                waypointPosition++;
                waypoint = path[waypointPosition];
            }
            else
            {
                OnReachedEnd?.Invoke(this, new OnReachedEndEventArgs() { reachedEnd = true });
            }
        }
    }
    public void Cancel() 
    {
        stop_Moving = true;
    }
}   