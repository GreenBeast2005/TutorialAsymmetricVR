using UnityEngine;

namespace Plugins.PanettoneGames.GenEvents.Demo.Scripts
{
    [RequireComponent(typeof(SphereCollider))]
    public class IntPublisher : MonoBehaviour
    {
        [SerializeField] private IntEvent intEvent;

        public int HitCounter { get; private set; }

        private void Awake()
        {
            GetComponent<SphereCollider>().isTrigger = true;
        }

        private void OnTriggerExit(Collider other)
        {
            HitCounter++;
            intEvent.Raise(HitCounter);
        }
    }
}