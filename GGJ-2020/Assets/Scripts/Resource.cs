using DG.Tweening;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] ResourceType resourceType;
    [SerializeField] int amount;

    Damageable damageable;
    [HideInInspector] public bool isHover;

    public Renderer[] renderers;

    private Color emissionColor;


    void Awake()
    {
        damageable = gameObject.GetComponent<Damageable>();
        damageable.onDestroy.AddListener(GiveResource);
        damageable.onHit.AddListener(HitResource);

        foreach (Renderer renderer in renderers)
        {
            if (renderer)
            {
                emissionColor = renderer.material.GetColor("_EmissionColor");
            }
        }
    }


    void GiveResource()
    {
        BuildingManager.instance.AddResource(resourceType, amount);
    }

    void HitResource()
    {
        transform.DOComplete();
        transform.DOShakeScale(.5f, .2f, 10, 90, true);
    }

    private void OnMouseEnter()
    {

        if (ActorManager.instance.allActors.Count == 0) return;
        isHover = true;
        // foreach (Renderer renderer in renderers)
        // {
        //     if (renderer)
        //     {
        //         renderer.material.SetColor("_EmissionColor", Color.red);
        //     }
        //     else
        //     {
        //         print("SHIIIT");
        //     }
        // }
        // transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

    }

    private void OnMouseExit()
    {
        if (ActorManager.instance.allActors.Count == 0) return;

        isHover = false;
        // foreach (Renderer renderer in renderers)
        // {
        //     if (renderer)
        //         renderer.material.SetColor("_EmissionColor", emissionColor);
        // }
        // transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }
}
