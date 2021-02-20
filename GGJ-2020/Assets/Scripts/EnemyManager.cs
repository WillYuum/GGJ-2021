using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance = null;


    private List<Actor> allEnemies = new List<Actor>();

    public GameObject[] spawnPoints;

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
        if (Input.GetKeyUp(KeyCode.E))
        {
            StartSpawningEnemies();
        }
    }

    public void StartSpawningEnemies()
    {
        GameObject enemy = SpawnManager.instance.GetEnemy();
        int randIndex = Random.Range(0, spawnPoints.Length - 1);
        Transform selectPositionToSpawn = spawnPoints[randIndex].transform;
        enemy.transform.position = selectPositionToSpawn.position;
    }

}
