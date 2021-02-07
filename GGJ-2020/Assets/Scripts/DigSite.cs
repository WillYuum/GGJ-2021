using DG.Tweening;
using UnityEngine;

public class DigSite : MonoBehaviour
{

    private int currentDigDone = 0;
    private int totalWorkToComplete = 10;
    private bool isHover;

    public bool isDigged = false;

    public void DoDig(int amount)
    {
        currentDigDone += amount;

        transform.DOComplete();
        transform.DOShakeScale(.5f, .2f, 10, 90, true);
    }

    public bool DigSiteIsFinished()
    {
        Debug.Log("Progress on Dig site: " + currentDigDone);
        if (currentDigDone >= totalWorkToComplete && isDigged == false)
        {
            isDigged = true;
        }
        return currentDigDone >= totalWorkToComplete;

    }



    private void OnMouseEnter()
    {
        isHover = true;
    }

    private void OnMouseExit()
    {
        isHover = false;
    }

}
