﻿using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ActorManager : MonoBehaviour
{

    public static ActorManager instance;
    [SerializeField] LayerMask actorLayer = default;
    [SerializeField] Transform selectionArea = default;
    public List<Actor> allActors = new List<Actor>();
    [SerializeField] List<Actor> selectedActors = new List<Actor>();
    Camera mainCamera;
    Vector3 startDrag;
    Vector3 endDrag;
    Vector3 dragCenter;
    Vector3 dragSize;
    bool dragging;

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


    void Start()
    {
        mainCamera = Camera.main;
        foreach (Actor actor in GetComponentsInChildren<Actor>())
        {
            allActors.Add(actor);
        }
        selectionArea.gameObject.SetActive(false);
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            dragging = false;
            return;
        }

        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;

        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         if (hit.collider.gameObject.layer == actorLayer)
        //         {
        //             Actor selectedCharacter = hit.collider.gameObject.GetComponent<Actor>();
        //             SelectOneCharacter(selectedCharacter);
        //         }
        //     }
        // }

        if (Input.GetMouseButtonDown(0))
        {
            startDrag = Utils.MouseToTerrainPosition();
            endDrag = startDrag;
        }
        else if (Input.GetMouseButton(0))
        {
            endDrag = Utils.MouseToTerrainPosition();

            if (Vector3.Distance(startDrag, endDrag) > 1)
            {
                selectionArea.gameObject.SetActive(true);
                dragging = true;
                dragCenter = (startDrag + endDrag) / 2;
                dragSize = (endDrag - startDrag);
                selectionArea.transform.position = dragCenter;
                selectionArea.transform.localScale = dragSize + Vector3.up;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (dragging)
            {
                SelectActors();
                dragging = false;
                selectionArea.gameObject.SetActive(false);

            }
            else
            {
                SetTask();
            }
        }

    }

    void SelectActors()
    {
        DeselectActors();
        dragSize.Set(Mathf.Abs(dragSize.x / 2), 1, Mathf.Abs(dragSize.z / 2));
        RaycastHit[] hits = Physics.BoxCastAll(dragCenter, dragSize, Vector3.up, Quaternion.identity, 0, actorLayer.value);
        Debug.Log(hits.Length);
        foreach (RaycastHit hit in hits)
        {
            Debug.Log("name: " + hit.collider.gameObject.name);
            if (hit.collider.TryGetComponent(out Actor actor))
            {
                selectedActors.Add(actor);
                actor.visualHandler.Select();
            }
        }
    }

    public void DeselectActors()
    {
        foreach (Actor actor in selectedActors)
            actor.visualHandler.Deselect();

        selectedActors.Clear();
    }

    public void SelectOneCharacter(Actor actor)
    {
        DeselectActors();
        selectedActors.Add(actor);
        actor.visualHandler.Select();
    }


    void SetTask()
    {
        Debug.Log("Setting task");
        if (selectedActors.Count == 0) return;

        Collider collider = Utils.CameraRay().collider;

        //NOTE: Make sure to add floor tag as terrain
        if (collider.CompareTag("Terrain"))
        {
            foreach (Actor actor in selectedActors)
            {
                actor.SetDestination(Utils.MouseToTerrainPosition());
            }
        }
        else if (!collider.TryGetComponent(out Damageable damageable))
        {
            foreach (Actor actor in selectedActors)
            {
                actor.AttackTarget(damageable);
            }
        }

    }


    private void OnDrawGizmos()
    {
        Vector3 center = (startDrag + endDrag) / 2;
        Vector3 size = (endDrag - startDrag);
        size.y = 1;
        Gizmos.DrawWireCube(center, size);
    }
}