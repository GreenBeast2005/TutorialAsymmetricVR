using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using PanettoneGames.GenEvents;
using UnityEngine;

public class MindMapManager : MonoBehaviour, IGameEventListener<GameObject>
{
    private List<List<GameObject>> graph;

    void Awake()
    {
        graph = new List<List<GameObject>>();
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
    public void OnEventRaised(GameObject item)
    {
        throw new System.NotImplementedException();
    }
}
