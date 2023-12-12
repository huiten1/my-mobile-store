using System;
using DG.Tweening;
using UnityEngine;

namespace _Main._Scripts.FX
{
    public class Bounce : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private float duration;
        private void Start()
        {
            transform
                .DOLocalMove(transform.localPosition+ offset, duration)
                .SetEase(Ease.OutSine)
                .SetLoops(-1,LoopType.Yoyo);
        }
    }
}