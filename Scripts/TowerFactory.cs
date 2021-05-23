using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] List<Tower> towerPrefab = new List<Tower>();
    [SerializeField] int maxTowers = 5;
    [SerializeField] Transform towerParentTransform;
    [SerializeField] GameObject arrowTest;

    public void AddTower(Waypoint baseWaypoint)
    {
        InstantiateNewTower(baseWaypoint);
    }

    void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab[Random.Range(0, towerPrefab.Count)], baseWaypoint.transform.position + new Vector3(0f, 4f, 0f), Quaternion.identity);
        newTower.transform.parent = baseWaypoint.transform;
        baseWaypoint.isPlaceable = false;
        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;
    }


    //  Queue<Tower> towerQueue = new Queue<Tower>();
    // public void AddTower(Waypoint baseWaypoint)
    //  {
    //      int numTowers = towerQueue.Count;
    //      if(numTowers < maxTowers)
    //      {
    //          InstantiateNewTower(baseWaypoint);
    //      }
    //      else
    //      {
    //          MoveExistingTower(baseWaypoint);
    //      }
    //  }
    //  private void InstantiateNewTower(Waypoint baseWaypoint)
    //  {
    //      var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
    //      newTower.transform.parent = towerParentTransform;
    //      baseWaypoint.isPlaceable = false;
    //
    //      newTower.baseWaypoint = baseWaypoint;
    //      baseWaypoint.isPlaceable = false;
    //
    //      towerQueue.Enqueue(newTower);
    //  }
    //  private  void MoveExistingTower(Waypoint newBaseWaypoint)
    //  {
    //      var oldTower = towerQueue.Dequeue();
    //
    //      oldTower.baseWaypoint.isPlaceable = true;
    //      newBaseWaypoint.isPlaceable = false;
    //
    //      oldTower.baseWaypoint = newBaseWaypoint;
    //      oldTower.transform.position = newBaseWaypoint.transform.position;
    //
    //      towerQueue.Enqueue(oldTower);
    //  }
}
