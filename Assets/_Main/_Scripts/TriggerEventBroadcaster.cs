using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Main._Scripts
{
    public class TriggerEventBroadcaster : MonoBehaviour
    {
        public string tagToCheck;
        public UnityEvent<GameObject> onEnter;
        public UnityEvent<GameObject> onStay;
        public UnityEvent<GameObject> onExit;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(tagToCheck))
            {
                onEnter?.Invoke(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(tagToCheck))
            {
                onExit?.Invoke(other.gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(tagToCheck))
            {
                onStay?.Invoke(other.gameObject);
            }
        }
    }
}