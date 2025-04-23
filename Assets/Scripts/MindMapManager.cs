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
    private List<GameObject> connections;
    private Dictionary<GameObject, List<GameObject>> map;


    void Awake()
    {
        map = new Dictionary<GameObject, List<GameObject>>();
        connections = new List<GameObject>();
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

// Need to make gameobject, gameobject event.
    public void OnEventRaised(GameObject item1, GameObject item2)
    {
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

        Spline spline = newLine.GetComponent<Spline>();
        spline.InsertNode(1, new SplineNode(item1.transform.position, UnityEngine.Vector3.zero));
        spline.InsertNode(2, new SplineNode(item2.transform.position, UnityEngine.Vector3.zero));

        connections.Add(newLine);

        foreach (var kvp in map)
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}
