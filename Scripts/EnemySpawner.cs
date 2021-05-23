
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float timeBeforeNextSpawn = 2f;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] Transform enemyTransformParent;
    [SerializeField] TMP_Text enemiesSpawnedText;
    [SerializeField] AudioClip enemySpawnSFX;

 
    int enemiesSpawned = 0;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while(true) //runs forever
        {
            GetComponent<AudioSource>().PlayOneShot(enemySpawnSFX); 
            var enemySpawned = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, Quaternion.identity);
            enemiesSpawned++;
            enemySpawned.transform.parent = enemyTransformParent;
            yield return new WaitForSeconds(timeBeforeNextSpawn);
        }   
    }
    private void Update()
    {
        enemiesSpawnedText.text = enemiesSpawned.ToString();
    }
}
