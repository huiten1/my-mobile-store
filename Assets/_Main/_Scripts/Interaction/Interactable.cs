using UnityEngine;
using UnityEngine.Events;

namespace _Main._Scripts.Interaction
{
    public class Interactable : MonoBehaviour,IInteractable
    {
        [SerializeField] private UnityEvent<GameObject> onInteract;
        public void Interact(GameObject interactor)
        {
            onInteract?.Invoke(interactor);
        }
    }
}