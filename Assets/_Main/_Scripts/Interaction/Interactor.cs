using System;
using _Main._Scripts.Money;
using UnityEngine;

namespace _Main._Scripts.Interaction
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float tickTime = 0.25f;
        private float t;
        private IInteractable currentInteractable;
        public bool NotInteract;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IInteractable>() != null)
            {
                currentInteractable = other.GetComponent<IInteractable>();
            }
        }

        private void Update()
        {
            if (NotInteract) return;
            
            if (currentInteractable == null)
            {
                return;
            }

            if (currentInteractable is MonoBehaviour behaviour)
            {
                if (!behaviour)
                {
                    currentInteractable = null;
                }
            }
            
            if (t > tickTime)
            {
                t = 0;
                
                currentInteractable?.Interact(gameObject);
                return;
            }
            t += Time.deltaTime;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IInteractable>() == null) return;

            if (other.GetComponent<IInteractable>() == currentInteractable) currentInteractable = null;
        }
    }
}