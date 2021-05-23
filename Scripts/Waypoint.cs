using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{   
    public bool isExplored = false;
    public Waypoint exploredFrom;
    public bool isPlaceable = true;
    public bool hasTower = false;
    
    const int gridSize = 10;

    public int GetGridSize()
    {
        return gridSize;
    }
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize)  ,// RoundToInt rounds the number when its more than .5 of its value
            Mathf.RoundToInt(transform.position.z / gridSize)  
            );
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            if (isPlaceable && !hasTower)
            {
                FindObjectOfType<TowerFactory>().AddTower(this); // Gets reference to the TowerFactory method and adds tower to current waypoint clicked    
                FindObjectOfType<ProcessBuying>().Buy(this);
            }
            else if (!isPlaceable && hasTower)
            {
                FindObjectOfType<ProcessBuying>().SellTower(this);
            }    
            else
            {
                print("You can not place a tower here!");
            }
        }
    }
}
