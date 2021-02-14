using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRange : MonoBehaviour
{
    [HideInInspector] public GameObject currentSelectedEnemy = null;
    [HideInInspector] public List<GameObject> enemiesInRange = new List<GameObject>();

    private float updateTargetDelay = 1.0f;

    private GameObject parentCannon;
    void Start()
    {
        parentCannon = transform.parent.gameObject;
    }

    public void StartCannonToAim()
    {
        Debug.LogWarning("Cannon started aiming");
        StartCoroutine(UpdateTarget());
    }


    private IEnumerator UpdateTarget()
    {
        yield return new WaitForSeconds(1);
        print("Enemies Count: " + enemiesInRange.Count);
        if (enemiesInRange.Count > 0)
        {
            SelectTarget();
        }
    }

    private void SelectTarget()
    {
        float minDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        enemiesInRange.ForEach(enemy =>
       {
           if (enemy == null)
           {
               bool isRemoved = enemiesInRange.Remove(enemy);
               if (isRemoved == false)
               {
                   Debug.LogError("Enemy in range wasn't removed in cannon range");
               }

           }
           else
           {
               float distanceToEnemy = Vector3.Distance(parentCannon.transform.position, enemy.transform.position);
               if (distanceToEnemy < minDistance)
               {
                   minDistance = distanceToEnemy;
                   nearestEnemy = enemy;
               }

               if (nearestEnemy != null)
               {
                   currentSelectedEnemy = nearestEnemy;
                   Debug.Log("Cannon: Selected new enemy target " + currentSelectedEnemy.name);
               }
           }
       });

        StartCoroutine(UpdateTarget());
    }


    private string enemyTag = "Enemy";
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            enemiesInRange.Add(other.gameObject);
            SelectTarget();
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            bool isRemoved = enemiesInRange.Remove(other.gameObject);
            if (isRemoved == false)
            {
                Debug.LogError(other.gameObject.name + " shouldn't be in the list");
            }
        }
    }
}
