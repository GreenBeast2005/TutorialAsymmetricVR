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
        // The nodes get stored in a dictionary so that connectsions can be quickly determined.
        // Once it gets time to start saving stuff, we probably dont want all the data of the gameobject,
        // Just position, text, and other connections, maybe color too at some point
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

    // This gets called when 2 mind nodes are touched together, it creates a line connection between the 2 nodes, it goes both ways
    // So technically there are 2 lines per set of nodes, but that shouldnt affect performance too much I hope.
    public void OnEventRaised(GameObject item1, GameObject item2)
    {
        if(map.ContainsKey(item1) && map[item1].Contains(item2)) {
            // Debug.Log("Destroying Connection");

            map[item1].Remove(item2);
            Destroy(connections[(item1.transform, item2.transform)]);
            connections[(item1.transform, item2.transform)] = null;
            return;
        }

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
        // Debug.Log("Creating Connecton");

        // foreach (var kvp in map)
        // {
        //     Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        // }
    }
}
