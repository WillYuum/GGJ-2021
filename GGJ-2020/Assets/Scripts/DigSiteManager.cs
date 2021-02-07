
using System.Collections.Generic;
using UnityEngine;

public class DigSiteManager : MonoBehaviour
{


    public static DigSiteManager instance;

    List<DigSite> allDigSites = new List<DigSite>();


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
