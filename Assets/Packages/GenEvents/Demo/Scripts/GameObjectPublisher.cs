using UnityEngine;

namespace Plugins.PanettoneGames.GenEvents.Demo.Scripts
{
    public class GameObjectPublisher : MonoBehaviour
    {
        [SerializeField] private GameObjectEvent gameObjectEvent;
        [SerializeField] private AudioClip sfx;

        public AudioClip SFX => sfx;

        private void Awake()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        private void OnTriggerExit(Collider other)
        {
            gameObjectEvent.Raise(gameObject);
        }
    }
}