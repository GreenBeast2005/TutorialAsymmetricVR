using UnityEngine;

namespace Plugins.PanettoneGames.GenEvents.Demo.Scripts
{
    public class CubeParticles : MonoBehaviour, IGameEventListener<GameObject>
    {
        [SerializeField] private GameObjectEvent gameObjectEvent;
        [SerializeField] private ParticleSystem particleFX;

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
            var particleFXMain = particleFX.main;
            particleFXMain.startColor = item.GetComponent<ColorChanger>().Color;

            if (!particleFX.isPlaying)
                particleFX.Play();
        }
    }
}