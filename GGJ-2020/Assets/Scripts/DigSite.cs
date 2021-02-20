using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DigSite : MonoBehaviour
{


    private int currentDigDone = 0;
    private int totalProgress = 45;
    private int delayToMakeProgress = 5;
    private bool isHover;

    public bool isDigged = false;

    public void StartDigging()
    {
        print("Digging has Started");
        StartCoroutine(EnemyManager.instance.StartSpawningEnemiesOnDigSite());
        StartCoroutine(Dig());

    }
    public IEnumerator Dig()
    {
        while (GameManager.instance.gameIsOn)
        {
            if (currentDigDone >= totalProgress)
            {
                DigSiteIsFinished();
                EnemyManager.instance.StopSpawningEnemiesOnDigSite();
                StopCoroutine(Dig());
            }

            yield return new WaitForSeconds(delayToMakeProgress);
            currentDigDone += 5;

            transform.DOComplete();
            transform.DOShakeScale(.5f, .2f, 10, 90, true);
        }
    }

    public void DigSiteIsFinished()
    {
        isDigged = true;
    }

    private void OnMouseEnter()
    {
        isHover = true;
    }

    private void OnMouseExit()
    {
        isHover = false;
    }

}
