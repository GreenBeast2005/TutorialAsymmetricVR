using UnityEngine;

namespace Plugins.PanettoneGames.GenEvents.Demo.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class SFXManager : MonoBehaviour,
        IGameEventListener<GameObject>
    {
        [SerializeField] private GameObjectEvent gameObjectEvent;
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            gameObjectEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameObjectEvent.UnregisterListener(this);
        }

        public void OnEventRaised(GameObject item)
        {
            var obj = item.GetComponent<GameObjectPublisher>();
            audioSource.PlayOneShot(obj.SFX);
        }
    }
}