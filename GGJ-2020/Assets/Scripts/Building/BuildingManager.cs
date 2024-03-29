﻿using System.Collections.Generic;
using UnityEngine;
public enum ResourceType { Wood, Rock }
public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;


    List<Building> allBuildings = new List<Building>();

    public Building[] buildingPrefabs = default;

    public int[] currentResources = default;
    BuildingUI ui;

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

    private void Start()
    {
        currentResources = new int[] { 0, 0 };
        ui = FindObjectOfType<BuildingUI>();
        if (ui)
            ui.RefreshResources();
    }


    public void SpawnBuilding(int index, Vector3 position)
    {
        Building building = buildingPrefabs[index];
        if (!building.CanBuild(currentResources))
            return;

        // Create Building
        building = Instantiate(buildingPrefabs[index], position, Quaternion.identity);
        allBuildings.Add(building);
        building.attackable.onDestroy.AddListener(() => RemoveBuilding(building));

        // Give builders build task
        foreach (Actor actor in ActorManager.instance.allActors)
        {
            if (actor is Pirate)
            {
                Pirate pirate = actor as Pirate;
                if (!pirate.HasTask())
                    pirate.GiveJob(building);
            }
        }

        // Subtract resources
        int[] cost = building.Cost();
        for (int i = 0; i < cost.Length; i++)
        {
            currentResources[i] -= cost[i];
            if (ui)
                ui.RefreshResources();
        }

    }


    public List<Building> GetBuildings()
    {
        return allBuildings;
    }


    public Building GetPrefab(int index)
    {
        return buildingPrefabs[index];
    }

    public Building GetRandomBuilding()
    {
        if (allBuildings.Count > 0)
            return allBuildings[Random.Range(0, allBuildings.Count)];
        else
            return null;
    }

    public void RemoveBuilding(Building building)
    {
        allBuildings.Remove(building);
    }


    public void AddResource(ResourceType resourceType, int amount)
    {
        currentResources[(int)resourceType] += amount;

        if (ui)
            ui.RefreshResources();
    }
    public void PlayParticle(Vector3 position)
    {
        // if (buildParticle)
        // {
        //     buildParticle.transform.position = position;
        //     buildParticle.Play();
        // }
    }

}
