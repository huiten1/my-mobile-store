using System;
using DG.Tweening;
using UnityEngine;

namespace _Main._Scripts.FX
{
    public class Bounce : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease = Ease.OutSine;
        private void Start()
        {
            transform
                .DOLocalMove(transform.localPosition+ offset, duration)
                .SetEase(ease)
                .SetLoops(-1,LoopType.Yoyo);
        }
    }
}