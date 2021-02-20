using UnityEngine;
using DG.Tweening;


public class Skeleton : Actor
{


    private GameObject selectedObjectToAttack = null;
    private bool isMovingTowardAttackableObject = true;
    public float damageInflictOnPirate = 5;
    private float hitRate = 2.0f;
    private float timeToNextHit = 0;

    private bool selectedDestination = false;

    private GameObject objectToAttack;
    private bool isAttacking = false;


    public bool shouldNotAttack = false;
    //Removeing this creates issues
    void Awake()
    {
    }

    void Start()
    {
        PickNearestPirate();
    }

    void Update()
    {
        if (shouldNotAttack) return;
        HandleStartAttacking();
    }


    private void HandleStartAttacking()
    {
        // print("hi");
        if (isAttacking)
        {
            //Get close to objective

            if (Time.time > timeToNextHit)
            {
                timeToNextHit = Time.time + hitRate;

                //HIT!!
            }

        }
        else
        {
            if (selectedDestination == false)
            {
                PickNearestPirate();
            }
            else
            {
                if (agent.remainingDistance <= 5)
                {
                    selectedDestination = false;
                }
            }
        }
    }

    private void PickNearestPirate()
    {
        if (shouldNotAttack) return;

        float minDistance = Mathf.Infinity;
        GameObject nearestPirate = null;

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
            selectedDestination = true;
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
