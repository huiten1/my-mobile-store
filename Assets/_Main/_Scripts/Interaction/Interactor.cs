using System;
using UnityEngine;

namespace _Main._Scripts.Interaction
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float tickTime = 0.25f;
        private float t;
        private IInteractable currentInteractable;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IInteractable>() != null)
            {
                currentInteractable = other.GetComponent<IInteractable>();
            }
        }

        private void Update()
        {
            if (currentInteractable == null)
            {
                return;
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