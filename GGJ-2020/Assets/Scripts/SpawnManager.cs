using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = null;
    public GameObject cannonBallPrefab;

    public GameObject skeletonPrefab;
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


    public GameObject enemyNodeHolder;
    public GameObject GetEnemy()
    {
        GameObject enemy = Instantiate(skeletonPrefab);
        enemy.transform.parent = enemyNodeHolder.transform;
        return enemy;
    }
}
