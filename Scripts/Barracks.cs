
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Tower
{
    [SerializeField] GameObject knight_Prefab;
    [SerializeField] List<SpawnPositionBehaviour> spawn_Locations = new List<SpawnPositionBehaviour>();

    List<SpawnPositionBehaviour> available_Spawn_Locations = new List<SpawnPositionBehaviour>();

    [SerializeField] int maxKnights = 3;

    [SerializeField] float time_To_Wait_For_Next_Spawn = 10f;

    float time_Since_Last_Spawn = Mathf.Infinity;
    List<Ally> currentKnights = new List<Ally>();
    GameObject sight_Range_Indicator;
    // Start is called before the first frame update
    void Start()
    {
        SetRangeIndicators();
    }
    void SetRangeIndicators()
    {
        Vector3 sight_value = new Vector3(attackRange, 0.0001f, attackRange);
        sight_Range_Indicator = transform.Find("Sight Range").gameObject;
        sight_Range_Indicator.transform.localScale = sight_value;
    }
    protected override void Update()
    {
        time_Since_Last_Spawn += Time.deltaTime;
        ValidateKnightExistance();
        ValidateSpawnLocationSpawnability();
        TestTimeForSpawning();
        base.Update();
    }

    private void ValidateSpawnLocationSpawnability()
    {
        if (spawn_Locations.Count == 0) return;
        for(int i = 0; i < spawn_Locations.Count; i++)
        {
            if(spawn_Locations[i].has_Unit == false)
            {
                available_Spawn_Locations.Add(spawn_Locations[i]);
            }
            else
            {
                available_Spawn_Locations.Remove(spawn_Locations[i]);
            }
        }
    }

    private void ValidateKnightExistance()
    {
        if (currentKnights.Count == 0) return;
        for(int i = 0; i< currentKnights.Count; i++)
        {
            if(currentKnights[i] == null)
            {
                currentKnights.Remove(currentKnights[i]);
            }
        }
    }

    private void TestTimeForSpawning()
    {
        if(time_Since_Last_Spawn > time_To_Wait_For_Next_Spawn)
        {
            if(currentKnights.Count < maxKnights)
            {
                SpawnKnight();
                time_Since_Last_Spawn = 0;
            }
        }
    }

    private void SpawnKnight()
    {
        int spawn_Pos_Index = Random.Range(0, available_Spawn_Locations.Count);
        var spawnLocation = available_Spawn_Locations[spawn_Pos_Index];
        var knight = Instantiate(knight_Prefab, transform.position, Quaternion.identity, spawnLocation.transform);
        currentKnights.Add(knight.GetComponent<Ally>());
        knight.transform.position = available_Spawn_Locations[spawn_Pos_Index].transform.position;
    }
}
