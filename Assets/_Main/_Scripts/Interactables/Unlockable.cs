using System;
using _Game.UI;
using _Main._Scripts.Interaction;
using _Main._Scripts.Provider;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


namespace _Main._Scripts.Money
{
    public class Unlockable : MonoBehaviour , IInteractable,IIndicator<ProgressData>
    {
        [SerializeField] private int price;
        [SerializeField] private GameObject prefabToUnlock;
        public bool shouldPopulate;
        [SerializeField] private GameObject itemToPopulate;
        private int currentPrice;
        [SerializeField] private UnityEvent onUnlock;
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
            //TODO:Refactor this into its own class
            
            MoneySystem.Instance.SpendMoney(1);
            var money = Instantiate(MoneySystem.Instance.moneyPf,interactor.transform.position + Vector3.up,interactor.transform.rotation);
            var seq = DOTween.Sequence();
            money.transform.localScale = Vector3.zero;
            seq.Append(money.transform.DOMoveY(transform.position.y, 0.6f));
            seq.Insert(0,money.transform.DOMoveX(transform.position.x, 0.6f).SetEase(Random.Range(0,1f)>0.5f? Ease.OutSine : Ease.InSine ));
            seq.Insert(0,money.transform.DOMoveZ(transform.position.z, 0.6f).SetEase(Random.Range(0,1f)>0.5f? Ease.InQuad : Ease.OutQuad));
            seq.Insert(0, money.transform.DOScale(1, 0.6f));
            seq.Append(money.transform.DOPunchScale(Vector3.one*0.2f,0.3f));
            seq.onComplete += () =>
            {
                // currentPrice -= 1;
                ValueChanged?.Invoke(Value);
                Destroy(money.gameObject);
            };
            
            currentPrice -= 1;
            
            if (currentPrice > 0) return;
            
            onUnlock?.Invoke();
            unlocked = true;
            gameObject.SetActive(false);
            
            var spawnedObject = Instantiate(prefabToUnlock, transform.position + prefabToUnlock.transform.position,prefabToUnlock.transform.
                rotation);
            spawnedObject.transform.DOPunchScale(Vector3.one * 0.1f, 0.6f, 1, 1f);
            
            if (spawnedObject.GetComponent<ItemMoneyTransaction>())
            {
                CashierProvider.Instance.Add(spawnedObject);
            }
            if (spawnedObject.GetComponent<Transaction>())
            {
                spawnedObject.GetComponent<Transaction>().filter = itemToPopulate.name;
            }
            if (spawnedObject.GetComponent<LimitedItemHolder>())
            {
                var holder = spawnedObject.GetComponent<LimitedItemHolder>();
                FreeIsleProvider.Instance.Add(holder);
                if (!shouldPopulate) return;
                for (int i = 0; i < holder.MaxItemCount; i++)
                {
                    holder.Add( Instantiate(itemToPopulate));
                }
            }
        }
        public ProgressData Value => new()
            { percentage = 1 - (float)currentPrice / price, text = currentPrice.ToString() };
        public event Action<ProgressData> ValueChanged;
    }
}