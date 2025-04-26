using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using PanettoneGames.GenEvents;
using SplineMesh;
using UnityEngine;

public class MindMapManager : MonoBehaviour, IDualGameEventListener<GameObject, GameObject>
{
    public DualGameObjectEvent mindMapEvent;
    public GameObject connectionPrefab;
    private Dictionary<(Transform, Transform), GameObject> connections;
    private Dictionary<GameObject, List<GameObject>> map;


    void Awake()
    {
        map = new Dictionary<GameObject, List<GameObject>>();
        connections = new Dictionary<(Transform, Transform), GameObject>();
    }

    void OnEnable()
    {
        mindMapEvent.RegisterListener(this);
    }
    void OnDisable() 
    {
        mindMapEvent.UnregisterListener(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void OnEventRaised(GameObject item1, GameObject item2)
    {
        // if(map.ContainsKey(item2) && map[item2].Contains(item1)) {
        //     return;
        // }

        if(map.ContainsKey(item1) && map[item1].Contains(item2)) {
            Debug.Log("Destroying Connection");

            map[item1].Remove(item2);
            Destroy(connections[(item1.transform, item2.transform)]);
            connections[(item1.transform, item2.transform)] = null;
            return;
        }

        // if(map.ContainsKey(item2) && map[item2].Contains(item1)) {
        //     Debug.Log("Destroying Connection");

        //     map[item2].Remove(item1);
        //     Destroy(connections[(item2.transform, item1.transform)]);
        //     connections[(item2.transform, item1.transform)] = null;
        //     return;
        // }


        // Check if the key exists in the dictionary
        if (!map.ContainsKey(item1))
        {
            // If it doesn't exist, add it with an empty list
            map[item1] = new List<GameObject>();
        }

        // need to add a way to check if these nodes already exist.
        // Now that the key exists, add item2 to the list
        map[item1].Add(item2);

        // Need to figure out this connection thing.
        GameObject newLine = Instantiate<GameObject>(connectionPrefab);

        MindMapConnection line = newLine.GetComponent<MindMapConnection>();
        line.pointA = item1.transform;
        line.pointB = item2.transform;

        connections[(item1.transform, item2.transform)] = newLine;
        Debug.Log("Creating Connecton");

        foreach (var kvp in map)
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}
