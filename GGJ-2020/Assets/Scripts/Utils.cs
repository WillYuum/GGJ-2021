using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 MouseToTerrainPosition()
    {
        Vector3 position = Vector3.zero;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info, 100, LayerMask.GetMask("Level")))
            position = info.point;
        return position;
    }

    public static RaycastHit CameraRay()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info, 100))
            return info;
        return new RaycastHit();
    }

    public static void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            T tmp = list[i];
            int randIndex = Random.Range(i, list.Count);  //By replacing 'i' with 0, you might get a more randomized array.
            list[i] = list[randIndex];
            list[randIndex] = tmp;
        }
    }

}

public class PseudoRandomList<T>
{
    private int counter = 0;
    public List<T> items = new List<T>();

    public PseudoRandomList(List<T> r_items, bool shuffle = false)
    {
        items = r_items;
        counter = 0;

        if (shuffle)
        {
            ShuffleList();
        }
    }

    private void ShuffleList()
    {
        System.Random randomizer = new System.Random();
        for (int i = items.Count - 1; i > 0; i--)
        {
            T tmp = items[i];
            int randIndex = randomizer.Next(i, items.Count);  //By replacing 'i' with 0, you might get a more randomized array.
            items[i] = items[randIndex];
            items[randIndex] = tmp;
        }

    }

    public T PickNext()
    {
        Debug.Log("Counter!!! " + counter);

        T selectedItem = items[counter];
        counter += 1;

        if (counter >= items.Count)
        {
            counter = 0;
            ShuffleList();
        }

        return selectedItem;
    }
}