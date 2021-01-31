using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [HideInInspector] public bool gameIsOn = false;


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



    public void ExitGame()
    {
        Application.Quit();
    }
}
