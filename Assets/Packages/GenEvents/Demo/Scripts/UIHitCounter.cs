using TMPro;
using UnityEngine;

namespace Plugins.PanettoneGames.GenEvents.Demo.Scripts
{
    public class UIHitCounter : MonoBehaviour, IGameEventListener<int>
    {
        [SerializeField] private IntEvent intEvent;
        private TextMeshProUGUI scoreText;

        private void Awake()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            intEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            intEvent.UnregisterListener(this);
        }

        public void OnEventRaised(int x)
        {
            scoreText.text = $"Hit {x} times";
            //Debug.Log($"{x} was recived by {gameObject.name}");
        }
    }
}