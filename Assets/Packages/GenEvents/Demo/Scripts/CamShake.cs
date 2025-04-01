using System.Collections;
using UnityEngine;

namespace Plugins.PanettoneGames.GenEvents.Demo.Scripts
{
    public class CamShake : MonoBehaviour,
        IGameEventListener<GameObject>
    {
        [SerializeField] [Range(0.05f, 1.5f)] private float intensity = 0.4f;
        [SerializeField] private GameObjectEvent gameObjectEvent;


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
            StartCoroutine(Shake(0.15f, intensity));
        }

        private IEnumerator Shake(float duration, float magnitude)
        {
            var originalPos = transform.localPosition;
            var elapsed = 0f;

            while (elapsed < duration)
            {
                var x = Random.Range(-1f, 1f) * magnitude;
                var y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPos.z);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = originalPos;
        }
    }
}