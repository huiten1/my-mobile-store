using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Main._Scripts
{
    public class NoChildBroadCaster : MonoBehaviour
    {
        public UnityEvent onNoChild;
        private bool fired;
        private void Update()
        {
            if (fired) return;

            if (transform.childCount == 0)
            {
                fired = true;
                onNoChild?.Invoke();
            }
        }
    }
}