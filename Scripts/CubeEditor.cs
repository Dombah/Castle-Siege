using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    void Update()   
    {
        SetGridPosition();
        UpdateLabel();
    }

    private void SetGridPosition()
    {
        int gridSize = waypoint.GetGridSize();

        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize,
            0f,
            waypoint.GetGridPos().y * gridSize); // .y is our z coordinate in 2D space
    }

    private void UpdateLabel()
    {
        TextMesh textMesh;
        textMesh = GetComponentInChildren<TextMesh>();
        string labelText = 
            waypoint.GetGridPos().x + // String that stores the cube cordinates
            "," +
            waypoint.GetGridPos().y; 
        textMesh.text = labelText;
        gameObject.name = labelText; // Sets the name of the cubes to the cordinates
    }
}
