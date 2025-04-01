using UnityEngine;

namespace Plugins.PanettoneGames.GenEvents.Demo.Scripts
{
    public class CamRotation : MonoBehaviour
    {
        [SerializeField] [Range(1, 20)] private float rotationSpeed = 5;

        private void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}