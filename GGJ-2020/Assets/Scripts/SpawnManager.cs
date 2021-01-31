using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = null;
    public GameObject cannonBallPrefab;
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
}
