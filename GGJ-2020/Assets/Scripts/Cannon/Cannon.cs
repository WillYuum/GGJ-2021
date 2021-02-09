using UnityEngine;

public class Cannon : MonoBehaviour
{

    public Transform firePoint;
    public float fireRate = 1.0f;
    public float shootForce = 2000.0f;
    private float timeTillNextShot = 0.0f;

    [SerializeField] private CannonRange shootRangeComp;
    private float secondsTillDestroyBullet = 5.0f;



    void Update()
    {
        AimAndShoot();
    }

    private void AimAndShoot()
    {
        if (shootRangeComp.currentSelectedEnemy != null)
        {
            //Handle rotation
            Vector3 dirToEnemy = shootRangeComp.currentSelectedEnemy.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dirToEnemy);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime).eulerAngles;

            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (Time.time > timeTillNextShot)
            {
                timeTillNextShot = Time.time + fireRate;
                bool isAimOnEnemy = Vector3.Dot(transform.forward, dirToEnemy.normalized) >= 0.9f;

                if (isAimOnEnemy)
                {
                    Shoot();
                }
            }
        }
    }


    private void Shoot()
    {
        Debug.Log("Fire cannon!");
        GameObject cannonBall = Instantiate(SpawnManager.instance.cannonBallPrefab, firePoint.position, Quaternion.identity);

        Vector3 shootDir = (transform.position - firePoint.position).normalized;
        shootDir.y = 0;
        cannonBall.GetComponent<Rigidbody>().AddForce(-shootDir * shootForce);

        Destroy(cannonBall, secondsTillDestroyBullet);
    }
}
