
using _Main._Scripts.Interaction;  
using UnityEngine;
using UnityEngine.Serialization;

namespace _Main._Scripts.Money
{
    public class Transaction : MonoBehaviour , IInteractable
    {
        [SerializeField] private ItemHolder itemholder;
        public string shouldGiveTo = "Untagged";
        [FormerlySerializedAs("giveFilter")] public string filter;
        public string shouldGetFrom = "Untagged";
        public void Interact(GameObject interactor)
        {
            var interactorHolder = interactor.GetComponent<ItemHolder>();
            if(!interactorHolder) return;
        
            if (interactor.CompareTag(shouldGiveTo) && !interactorHolder.IsFull)
            {
                if (filter != "")
                {
                    interactorHolder.Add(itemholder.Get(filter,interactor));
                    return;
                }
                interactorHolder.Add(itemholder.Pop());
                return;
            }
            if (interactor.CompareTag(shouldGetFrom) && !itemholder.IsFull)
            {
                if (filter != "")
                {
                    itemholder.Add(interactorHolder.Get(filter,interactor));
                    return;
                }
                itemholder.Add(interactorHolder.Pop());
                return;
            }
        }

        private void OnGUI()
        {
        }
    }
}