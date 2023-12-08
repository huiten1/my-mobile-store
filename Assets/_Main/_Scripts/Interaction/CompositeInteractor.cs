using UnityEngine;
using UnityEngine.Serialization;

namespace _Main._Scripts.Interaction
{
    public class CompositeInteractor : MonoBehaviour,IInteractable
    {
        [SerializeField] private Interactable[] interactables;
        public void Interact(GameObject interactor)
        {
            foreach (var interactable in interactables)
            {
                interactable.Interact(interactor);        
            }
        }
    }
}