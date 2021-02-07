using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Pirate : Actor
{


    Building currentBuilding;

    DigSite currentSiteDigging;

    private void Start()
    {

        animationEvent.attackEvent.AddListener(DoWork);
    }

    public void GiveJob(Building job)
    {
        currentBuilding = job;

        if (currentTask != null)
            StopCoroutine(currentTask);

        currentTask = StartCoroutine(StartJob());
        IEnumerator StartJob()
        {
            Vector3 jobPosition = job.transform.position;
            Vector2 randomPosition = Random.insideUnitCircle.normalized * currentBuilding.radius;
            jobPosition.x += randomPosition.x;
            jobPosition.z += randomPosition.y;
            SetDestination(jobPosition);
            yield return WaitForNavMesh();
            transform.LookAt(currentBuilding.transform);
            while (!currentBuilding.IsFinished())
            {
                yield return new WaitForSeconds(1);
                if (!currentBuilding.IsFinished())
                {
                    //Animation was supposed to invoke AttackEvent
                    // animator.SetTrigger("Attack");
                    animationEvent.AttackEvent();
                }
            }
            currentBuilding = null;
            currentTask = null;
            Debug.Log("Done from building " + job.gameObject.name);
        }
    }


    public void TakeDamage()
    {
        transform.DOComplete();
        transform.DOShakeScale(.5f, .2f, 10, 90, true);
    }


    public bool HasTask()
    {
        return currentTask != null;
    }
    override public void StopTask()
    {
        base.StopTask();
        currentBuilding = null;
    }

    void DoWork()
    {
        Debug.Log("building");
        if (currentBuilding)
            currentBuilding.Build(10);
    }


    public override void StartInterActWithDigSite(DigSite digSite)
    {
        if (currentTask != null)
            StopCoroutine(currentTask);
        currentSiteDigging = digSite;
        currentTask = StartCoroutine(StartJob());
        IEnumerator StartJob()
        {
            while (!currentSiteDigging.DigSiteIsFinished())
            {
                //NOTE: Should add dig delay
                yield return new WaitForSeconds(1);
                if (!currentSiteDigging.DigSiteIsFinished())
                {
                    //Animation was supposed to invoke AttackEvent
                    // animator.SetTrigger("Attack");
                    // animationEvent.AttackEvent();
                    currentSiteDigging.DoDig(1);
                }
            }

            currentTask = null;
        }

    }



}
