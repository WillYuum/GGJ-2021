using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [HideInInspector] public bool gameIsOn = false;


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
        StartGame();
    }

    public void StartGame()
    {
        GameManager.instance.gameIsOn = true;
        DelayToStart(15);
        StartCoroutine(EnemyManager.instance.StartSpawnEnemiesRandomly());
    }

    private WaitForSeconds DelayToStart(int v)
    {
        return new WaitForSeconds(v);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
