using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{


    [HideInInspector] public UnityEvent onDestroy = new UnityEvent();
    [HideInInspector] public UnityEvent onHit = new UnityEvent();
    public float startingHealth = 100;
    [HideInInspector] public float currentHealth;


    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void Hit(float amount)
    {
        onHit.Invoke();
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            DestroyGameObject();
        }
    }


    void DestroyGameObject()
    {
        onDestroy.Invoke();
        Destroy(gameObject);
    }
}
