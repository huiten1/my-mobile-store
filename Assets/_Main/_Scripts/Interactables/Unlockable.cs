using System;
using _Game.UI;
using _Main._Scripts.Interaction;
using UnityEngine;

namespace _Main._Scripts.Money
{
    public class Unlockable : MonoBehaviour , IInteractable,IIndicator<ProgressData>
    {
        [SerializeField] private int price;
        [SerializeField] private LimitedItemHolder prefabToUnlock;
        public bool shouldPopulate;
        [SerializeField] private GameObject itemToPopulate;
        private int currentPrice;

        private bool unlocked;
        private void Start()
        {
            currentPrice = price;
            ValueChanged?.Invoke(Value);
        }
        public void Interact(GameObject interactor)
        {
            if(unlocked) return;
            if(!interactor.CompareTag("Player")) return;
            currentPrice -= 1;
            ValueChanged?.Invoke(Value);
            MoneySystem.Instance.SpendMoney(1);
            
            if (currentPrice > 0) return;
            
            unlocked = true;
            gameObject.SetActive(false);
            var holder =Instantiate(prefabToUnlock, transform.position + prefabToUnlock.transform.position,prefabToUnlock.transform.
                rotation);
            holder.GetComponent<Transaction>().filter = itemToPopulate.name;
            
            if (!shouldPopulate) return;
            for (int i = 0; i < holder.MaxItemCount; i++)
            {
                holder.Add( Instantiate(itemToPopulate));
            }
        }
        public ProgressData Value => new()
            { percentage = 1 - (float)currentPrice / price, text = currentPrice.ToString() };
        public event Action<ProgressData> ValueChanged;
    }
}