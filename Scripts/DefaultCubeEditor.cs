using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class DefaultCubeEditor : MonoBehaviour
{
    const int gridSize = 10;
    [SerializeField] int y = 4;
    void Update()   
    {
        SetGridPosition();
    }

    private void SetGridPosition()
    {
        transform.position = new Vector3(
            GetGridPos().x * gridSize,
            y,
            GetGridPos().y * gridSize); // .y is our z coordinate in 2D space
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }
}
