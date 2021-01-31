using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private string enemyTag = "Enemy";
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            other.gameObject.GetComponent<Damageable>().Hit(15.0f);
            Destroy(gameObject);
        }
    }
}
