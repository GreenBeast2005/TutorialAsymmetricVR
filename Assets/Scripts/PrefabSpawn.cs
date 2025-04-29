// Attach this script to any clickable GameObject
using UnityEngine;

public class PrefabSpawn : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign in Inspector
    public Transform spawnLocation;  // Optional: set where the prefab should spawn

    // This function is responsible for spawning the mind map nodes
    public void SpawnPrefab()
    {
        if (prefabToSpawn != null)
        {
            Vector3 spawnPos = spawnLocation != null ? spawnLocation.position : transform.position + Vector3.up;
            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        }
    }
}
