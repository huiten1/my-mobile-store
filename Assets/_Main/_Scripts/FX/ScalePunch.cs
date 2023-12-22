using System;
using DG.Tweening;
using UnityEngine;

namespace _Main._Scripts.FX
{
    public class ScalePunch : MonoBehaviour
    {
        public float startScale = 1f;
        public float punchScale = 0.3f;
        public float duration = 0.3f;
        private void Start()
        {
            transform.DOScale( Vector3.one, duration)
                .SetEase(Ease.OutBack,1+punchScale)
                .From(Vector3.one * startScale);
        }
    }
}