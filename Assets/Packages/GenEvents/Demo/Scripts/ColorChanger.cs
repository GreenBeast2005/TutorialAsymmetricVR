using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Plugins.PanettoneGames.GenEvents.Demo.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] private Color[] colors;
        [SerializeField] [Range(0.1f, 2f)] private float fadeSpeed = 0.5f;
        private Coroutine cr;
        private int currentindex;
        private int lastIndex;
        private Renderer rend;

        public Color Color { get; private set; }

        private void Awake()
        {
            rend = GetComponent<Renderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            while (currentindex == lastIndex)
                currentindex = Random.Range(0, colors.Length - 1);

            Color = colors[currentindex];
            rend.material.color = Color;

            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", Color);
            if (cr != null)
                StopCoroutine(cr);
            cr = StartCoroutine(FadeEmission(Color));
            lastIndex = Array.IndexOf(colors, Color);
        }

        private IEnumerator FadeEmission(Color c)
        {
            float elapsedTime = 0;
            float intensity;

            while (elapsedTime < fadeSpeed)
            {
                intensity = Mathf.Lerp(2, 0, elapsedTime / fadeSpeed);
                elapsedTime += Time.deltaTime;
                rend.material.SetColor("_EmissionColor", c * intensity);
                yield return null;
            }

            rend.material.SetColor("_EmissionColor", c * 0);

            yield return null;
        }
    }
}