 using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using DG.Tweening;

using UnityEngine;

namespace _Main._Scripts
{
    public class ItemMoneyTransaction : MonoBehaviour
    {
        [SerializeField] private Spawner.Spawner moneySpawn;
        [SerializeField] private Transform itemPlacement;
        [SerializeField] private Transform customerInteractionTf;
        public Vector3 customerInteractionPoint => customerInteractionTf.position;
        private bool hasCashier;

        private List<Item> proccessingItems = new();

        public List<GameObject> waitingCustomers = new List<GameObject>();

        private int queueCounter = 0;
        public void CustomerInteraction(GameObject customerGameObject)
        {
            if(!customerGameObject.CompareTag("Customer")) return;
            if(!hasCashier) return;
            
            var itemHolder = customerGameObject.GetComponent<ItemHolder>();
            var itemGO = itemHolder.Pop();

            if (itemGO == null)
            {
                if(queueCounter!=0) return;
                foreach (var itemToGiveBack in proccessingItems)
                {
                    itemHolder.Add(itemToGiveBack.gameObject);
                    Destroy(itemToGiveBack);
                }
                customerGameObject.GetComponent<BehaviorTree>().SendEvent("TransactionComplete");
                proccessingItems.Clear();
                customerGameObject.tag = "Untagged";
                waitingCustomers.RemoveAt(0);
                return;
            }
            
            customerGameObject.GetComponent<BehaviorTree>().SendEvent("AtCashier");
            var item = itemGO.GetComponent<Item>();
            if(!item || item==null) return;
            
            proccessingItems.Add(item);
            var moneyPf = Resources.Load<global::Money>("Money");

            queueCounter++;
            item.transform.DOMove(itemPlacement.position + Vector3.up*(proccessingItems.Count-1), 1f).onComplete += () =>
            {
                queueCounter--;
                int moneyPrice = 5; 
                int cashCount = item.price / moneyPrice;
                for (int i = 0; i < cashCount; i++)
                {
                    moneySpawn.Spawn(moneyPf, 1, (money) => money.moneyAmount = moneyPrice);
                }
            };
        }

        public void CashierInteraction(GameObject cashier)
        {
            hasCashier = cashier.CompareTag("Player");
        }

        public void CashierExited(GameObject cashier)
        {
            hasCashier = false;
        }
    }
}