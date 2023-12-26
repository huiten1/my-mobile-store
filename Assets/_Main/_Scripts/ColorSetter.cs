using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace _Main._Scripts
{
    public class ColorSetter : MonoBehaviour
    {
        [SerializeField] private Color[] colors;
        [SerializeField] private RenderAndMatIdx[] renderers;

        [Serializable]
        public struct RenderAndMatIdx
        {
            public Renderer renderer;
            public int[] materialIndexes;
        }

        private void Start()
        {
            var col = colors[Random.Range(0, colors.Length)];
            foreach (var rndr in renderers)
            {
                foreach (int index in rndr.materialIndexes)
                {
                    rndr.renderer.materials[index].SetColor("_BaseColor", col);
                }
            }
        }
    }
}