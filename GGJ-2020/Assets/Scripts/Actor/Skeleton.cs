using UnityEngine;
using DG.Tweening;


public class Skeleton : Actor
{


    private GameObject selectedObjectToAttack = null;
    private bool isMovingTowardAttackableObject = true;
    public float damageInflictOnPirate = 5;
    private float hitRate = 2.0f;
    private float timeToNextHit = 0;
    private void Awake()
    {
    }

    private void Start()
    {
        PickNearestPirate();
    }

    private void LateUpdate()
    {
        if(selectedObjectToAttack == null) return;
        
        float distToAttackableObject = Vector3.Distance(transform.position, selectedObjectToAttack.transform.position);
        if (distToAttackableObject <= agent.stoppingDistance)
        {
            isMovingTowardAttackableObject = false;
            if (Time.time > timeToNextHit)
            {
                timeToNextHit = Time.time + hitRate;
                if (selectedObjectToAttack.TryGetComponent(out Damageable damageable))
                {
                    print("hitting pirate");
                    damageable.Hit(damageInflictOnPirate);
                }
            }
        }
        else if (isMovingTowardAttackableObject == false)
        {
            PickNearestPirate();
        }
    }


    private void PickNearestPirate()
    {
        float minDistance = Mathf.Infinity;
        GameObject nearestPirate = null;
        print(ActorManager.instance.allActors.Count);
        ActorManager.instance.allActors.ForEach(pirate =>
        {
            float distanceToPirate = Vector3.Distance(transform.position, pirate.transform.position);
            if (distanceToPirate < minDistance)
            {
                nearestPirate = pirate.gameObject;
                selectedObjectToAttack = pirate.gameObject;
                minDistance = distanceToPirate;
            }
        });

        if (nearestPirate != null)
        {
            isMovingTowardAttackableObject = true;
            SetDestination(nearestPirate.transform.position);
        }
        else
        {
            Debug.Log("ffffuck");
        }
    }

    private void TakeDamage()
    {
        transform.DOComplete();
        transform.DOShakeScale(.5f, .2f, 10, 90, true);
    }
}
