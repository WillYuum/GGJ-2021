using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance = null;


    private List<Actor> allEnemies = new List<Actor>();

    public GameObject[] spawnPoints;

    //---------Delay to spawn enemies--------
    private float startingDelayToSpawnEnemy = 3;
    private float startingDelayToSpawnRandomEnemy = 15;
    private float delayToSpawnEnemy;
    private float delayToSpawnRandomEnemy;
    //-------------------------------------------


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

    void Start()
    {
        delayToSpawnEnemy = startingDelayToSpawnEnemy;
        delayToSpawnRandomEnemy = startingDelayToSpawnRandomEnemy;
    }

    private PseudoRandomArray<int> amountOfSpawnPoints = new PseudoRandomArray<int>(new List<int>() { 3, 4, 5, 6 }, true);

    public IEnumerator StartSpawningEnemiesOnDigSite(DigSite digSite)
    {
        int amountOfEnemyToSpawn = 5;
        int amountEnemySpawned = 0;

        Vector3 digSitePos = digSite.transform.position;

        List<Vector3> allSpawnPoints = new List<Vector3>();

        int selectedAmountSpawnPoints = amountOfSpawnPoints.PickNext();

        for (int i = 0; i < selectedAmountSpawnPoints; i++)
        {
            float radius = Random.Range(10, 35);
            Vector3 newPos = GetRandomPosAroundDigSite(digSite.transform.position, radius);
            allSpawnPoints.Add(newPos);
            // allSpawnPoints.Add(positionToSpawn);
        }

        PseudoRandomArray<Vector3> positionToSpawn = new PseudoRandomArray<Vector3>(allSpawnPoints, true);

        delayToSpawnRandomEnemy *= 2;
        while (GameManager.instance.gameIsOn)
        {
            yield return new WaitForSeconds(delayToSpawnEnemy);
            amountEnemySpawned += 1;
            if (amountOfEnemyToSpawn == amountEnemySpawned)
            {
                StopCoroutine(StartSpawningEnemiesOnDigSite(digSite));
            }

            Vector3 selectedPostionToSpawn = positionToSpawn.PickNext();
            SpawnEnemy(selectedPostionToSpawn);
            print("Spawned dig site enemy");
        }
    }

    private Vector3 GetRandomPosAroundDigSite(Vector3 center, float radius)
    {
        //ENHANCE: Can traverse through 360 degrees to make sure to 
        //spawn all around the center.
        float ang = Random.value * 360;
        Vector3 newPos;
        newPos.x = center.x + radius * Mathf.Sin(ang * Mathf.Rad2Deg);
        newPos.y = 0;
        newPos.z = center.z + radius * Mathf.Cos(ang * Mathf.Rad2Deg);
        return newPos;
    }

    public void StopSpawningEnemiesOnDigSite(DigSite digSite)
    {
        StopCoroutine(StartSpawningEnemiesOnDigSite(digSite));
        delayToSpawnRandomEnemy = startingDelayToSpawnRandomEnemy;
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