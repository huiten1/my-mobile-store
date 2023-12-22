
using System.Collections.Generic;
using _Main._Scripts.GameSequence;
using BehaviorDesigner.Runtime;
using DG.Tweening;

using UnityEngine;
using UnityEngine.Events;

namespace _Main._Scripts
{
    public class ItemMoneyTransaction : MonoBehaviour
    {
        [SerializeField] private Spawner.Spawner moneySpawn;
        [SerializeField] private Transform itemPlacement;
        [SerializeField] private Transform customerInteractionTf;
        [SerializeField] private GameObject boxPf;
        public Vector3 customerInteractionPoint => customerInteractionTf.position;
        private bool hasCashier;

        private List<GameObject> proccessingItems = new();

        public List<GameObject> waitingCustomers = new();

        private int queueCounter = 0;

        public UnityEvent onTransactionComplete;

        private GameObject currentCustomer;
        public void CustomerInteraction(GameObject customerGameObject)
        {
            if(!customerGameObject.CompareTag("Customer")) return;
            if(!hasCashier) return;
            if(waitingCustomers.Count==0) return;
            if(customerGameObject!=waitingCustomers[0]) return;
            
            if (currentCustomer == null)
            {
                currentCustomer = customerGameObject;
            }
            
            var itemHolder = customerGameObject.GetComponent<ItemHolder>();
            var itemGO = itemHolder.Pop();

            if (itemGO == null)
            {
                if(queueCounter!=0) return;
                foreach (var itemToGiveBack in proccessingItems)
                {
                    
                    itemHolder.Add(itemToGiveBack.gameObject);
                    Destroy(itemToGiveBack.GetComponentInChildren<Item>());
                }
                customerGameObject.GetComponent<BehaviorTree>().SendEvent("TransactionComplete");
                proccessingItems.Clear();
                customerGameObject.tag = "Untagged";
                waitingCustomers.RemoveAt(0);
                onTransactionComplete?.Invoke();
                currentCustomer = null;
                return;
            }
            
            customerGameObject.GetComponent<BehaviorTree>().SendEvent("AtCashier");
            var item = itemGO.GetComponent<Item>();
            if(!item || item==null) return;

            Destroy( item.GetComponentInChildren<Collider>());
            
            var moneyPf = Resources.Load<global::Money>("Money");
            queueCounter++;

            var box = Instantiate(boxPf, itemPlacement.position, boxPf.transform.rotation); 
            item.transform.SetParent(box.transform,true);
            
            Quaternion itemTargetRot = Quaternion.Euler(item.insideBoxRotation) * itemPlacement.rotation;
            var itemCol = item.GetComponentInChildren<Renderer>();
            Vector3 targetLocalPos = itemTargetRot * (item.transform.position - itemCol.bounds.center);
            targetLocalPos.y = -0.1f;
            var boxBounds = box.GetComponentInChildren<Collider>().bounds;
            
            bool cannotFit = false;
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (boxBounds.size[j] < itemCol.bounds.size[i])
                    {
                        cannotFit = true;
                        break;
                    }
                }
                if(cannotFit)
                    break;
            }
            if (cannotFit)
            {
                // float x
                item.transform.DOScale(item.transform.localScale* 0.8f, 0.6f);
            }
            proccessingItems.Add(box);
            float itemPlaceDuration = .5f;
            box.transform.localScale = Vector3.zero;

            var seq = DOTween.Sequence(box.transform.DOScale(boxPf.transform.localScale,0.6f).SetEase(Ease.OutBack));
            seq.Append(item.transform.DOLocalJump( -targetLocalPos, 1.5f,1,itemPlaceDuration));
            seq.Insert(0, item.transform.DORotateQuaternion(itemTargetRot, itemPlaceDuration).SetEase(Ease.InQuad));
            // seq.Insert(0, item.transform.DOScale(Vector3.one, itemPlaceDuration));
            seq.Insert(itemPlaceDuration*0.8f,box.transform.GetChild(1).DOLocalJump(Vector3.up*0.13f,1f,1,0.5f ));
            seq.Insert(itemPlaceDuration*0.8f,box.transform.GetChild(1).DOLocalRotate(Vector3.zero,0.5f ).SetEase(Ease.OutQuad));
            
            
            seq.onComplete += () =>
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