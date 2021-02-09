using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    CanvasGroup canvasGroup;
    bool isPlacing = false;
    int currentIndex = 0;
    public Transform resourceGroup;

    List<Mesh> buildingPreviewMesh = new List<Mesh>();
    [SerializeField] Material buildingPreviewMat;


    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(() => SelectBuilding(index));

            Building b = BuildingManager.instance.buildingPrefabs[index];

            // buttons[index].GetComponentInChildren<TextMeshProUGUI>().text = GetButtonText(b);
        }
    }

    private void Update()
    {
        if (isPlacing)
        {
            Vector3 position = Utils.MouseToTerrainPosition();
            foreach (Mesh item in buildingPreviewMesh)
            {
                Graphics.DrawMesh(item, position, Quaternion.identity, buildingPreviewMat, 0);
            }
            // Graphics.DrawMesh(buildingPreviewMesh, position, Quaternion.identity, buildingPreviewMat, 0);
            if (Input.GetMouseButtonDown(0))
            {
                BuildingManager.instance.SpawnBuilding(currentIndex, position);
                // canvasGroup.alpha = 1;
                isPlacing = false;
            }
        }
    }


    void SelectBuilding(int index)
    {
        currentIndex = index;
        ActorManager.instance.DeselectActors();
        // canvasGroup.alpha = 0;
        buildingPreviewMesh.Clear();
        isPlacing = true;
        MeshFilter[] x = BuildingManager.instance.GetPrefab(index).GetComponent<Building>().allMeshForm;
        foreach (MeshFilter item in x)
        {
            // buildingPreviewMesh.
            buildingPreviewMesh.Add(item.sharedMesh);
        }

    }


    public TextMeshProUGUI[] resourcesIndicatorsUI;
    public void RefreshResources()
    {
        int count = resourcesIndicatorsUI.Length;
        for (int i = 0; i < count; i++)
            resourcesIndicatorsUI[i].text = BuildingManager.instance.currentResources[i].ToString();
    }
}