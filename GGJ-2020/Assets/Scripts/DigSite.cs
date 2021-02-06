using UnityEngine;

public class DigSite : MonoBehaviour
{
    private int countTillDigDone = 10;
    public void DoDig(int amount)
    {
        countTillDigDone -= amount;
    }
}
