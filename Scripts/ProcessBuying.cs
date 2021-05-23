using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessBuying : MonoBehaviour
{
    [SerializeField] float sell_Percentage = 0.8f; // Percentage of the whole value payed returned when selling

    public void Buy(Waypoint baseWaypoint)
    {
        if(baseWaypoint.hasTower == true) { return; }
        baseWaypoint.transform.Find("Pillar").gameObject.SetActive(false);
        baseWaypoint.hasTower = true;
    }

    public void SellTower(Waypoint baseWaypoint)
    {
        var temp = baseWaypoint.GetComponentInChildren<Tower>();
        Destroy(temp.gameObject);
        baseWaypoint.transform.Find("Pillar").gameObject.SetActive(true);
        baseWaypoint.hasTower = false;
        baseWaypoint.isPlaceable = true;
    }
}
