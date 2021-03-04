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

    public GameObject drillMachine;

    public bool isDigging = false;

    public float drillMachineRotSpeed = 280;
    void Start()
    {
        drillMachine.SetActive(false);
    }

    void Update()
    {
        if (isDigging)
        {
            drillMachine.transform.Rotate(new Vector3(0, drillMachineRotSpeed * Time.deltaTime, 0), Space.World);
        }
    }

    public void StartDigging()
    {
        if (isDigging) return;

        print("Digging has Started");
        isDigging = true;
        StartCoroutine(EnemyManager.instance.StartSpawningEnemiesOnDigSite(this));
        StartCoroutine(Dig());
        drillMachine.SetActive(true);

    }

    public IEnumerator Dig()
    {
        while (GameManager.instance.gameIsOn)
        {
            if (currentDigDone >= totalProgress)
            {
                EnemyManager.instance.StopSpawningEnemiesOnDigSite(this);
                StopCoroutine(Dig());
                DigSiteIsFinished();
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
        isDigging = false;
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
