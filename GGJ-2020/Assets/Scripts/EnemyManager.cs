using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance = null;


    private List<Actor> allEnemies = new List<Actor>();

    public GameObject[] spawnPoints;

    private float delayToSpawnEnemy = 2;
    private float delayToSpawnRandomEnemy = 8;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // if (Input.GetKeyUp(KeyCode.E))
        // {
        //     SpawnEnemy();
        // }
    }


    public IEnumerator StartSpawningEnemiesOnDigSite()
    {
        while (GameManager.instance.gameIsOn)
        {
            yield return new WaitForSeconds(delayToSpawnEnemy);
            // SpawnEnemy();
        }
    }


    public IEnumerator StartSpawnEnemiesRandomly()
    {
        print("Started spawning enemies randomly");
        while (GameManager.instance.gameIsOn)
        {
            yield return new WaitForSeconds(delayToSpawnRandomEnemy);
            int randIndex = Random.Range(0, spawnPoints.Length - 1);
            Transform selectPositionToSpawn = spawnPoints[randIndex].transform;
            SpawnEnemy(selectPositionToSpawn.position);
        }
    }

    public void SpawnEnemy(Vector3 position)
    {
        GameObject enemy = SpawnManager.instance.GetEnemy();
        enemy.transform.position = position;
    }
}
