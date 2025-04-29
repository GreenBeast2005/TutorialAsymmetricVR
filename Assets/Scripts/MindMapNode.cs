using System.Collections;
using System.Collections.Generic;
using PanettoneGames.GenEvents;
using UnityEngine;

public class MindMapNode : MonoBehaviour
{
    public LayerMask targetLayer;
    public DualGameObjectEvent mindMapEvent;

    // Sends an event to the mind map manager that a connection has been created when it touches another MindNode.
    void OnTriggerEnter(Collider other)
    {
        if((targetLayer.value & (1 << other.gameObject.layer)) != 0) {
            mindMapEvent.Raise(this.gameObject, other.gameObject);
        }
    }
}
