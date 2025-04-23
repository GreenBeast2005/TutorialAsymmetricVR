using System.Collections;
using System.Collections.Generic;
using PanettoneGames.GenEvents;
using UnityEngine;

public class MindMapNode : MonoBehaviour
{
    public LayerMask targetLayer;
    public DualGameObjectEvent mindMapEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if((targetLayer.value & (1 << other.gameObject.layer)) != 0) {
            mindMapEvent.Raise(this.gameObject, other.gameObject);
        }
    }
}
