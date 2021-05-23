using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;                     
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>(); // All waypoints in scene with their cordinates
    Queue<Waypoint> queue = new Queue<Waypoint>();      
    Waypoint searchCentre; // The waypoint from which the searching starts
    List<Waypoint> path = new List<Waypoint>(); // The calculated path  
    Vector2Int[] directions =           // All possible directions
    {
       Vector2Int.up,
       Vector2Int.right,
       Vector2Int.down,
       Vector2Int.left
    };
    bool isRunning = true;  
    public List<Waypoint> GetPath()
    {

        if(path.Count == 0)
        {
            CalculatePath();
        }
        return path;
        
    }
    private void CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
    }
    private void LoadBlocks() // Find all waypoints in scene and add them to grid
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if (isOverlapping)
            {
                Debug.LogWarning("Overlapping " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);

            }
        }
    }
    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);       // Add first waypoint to search queue
        while (queue.Count > 0 && isRunning) // Search while patch isn't reached
        {
            searchCentre = queue.Dequeue(); 
            HaltIfEndReached();
            ExploreNeighbours();
            searchCentre.isExplored = true;
        }
    }
    private void HaltIfEndReached() // When the searchCentre is the same as the endWaypoint stop searching
    {
        if (searchCentre == endWaypoint)
        {
            isRunning = false;
        }
    }
    private void ExploreNeighbours() // Search for waypoints from search centre
    {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = direction + searchCentre.GetGridPos();
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }
    private void CreatePath() 
    {
        SetAsPath(endWaypoint);
        Waypoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
           
        }
        SetAsPath(startWaypoint);
        path.Reverse();  
    }
    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }
    private void QueueNewNeighbours(Vector2Int neighbourCoordinates) 
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            // do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCentre;
        }      
    }
}
