using System;
using System.Collections;
using _Game.UI;
using _Main._Scripts.Interaction;
using _Main._Scripts.Provider;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent;
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

        private string key = "";
        
        private IEnumerator Start()
        {
            GenerateKey();
            currentPrice = GetPrice();
            UnityEngine.Debug.Log(key + currentPrice);
            ValueChanged?.Invoke(Value);
            yield return null;
            if (currentPrice <= 0)
            {
                Unlock();
            }
            GameManager.Instance.stageReset += Reset;
        }

        private void GenerateKey()
        {
            var tf = transform;
            while (tf.parent != null)
            {
                key = key.Insert( 0,$"{tf.gameObject.name}.");
                tf = tf.parent;
            }
        }

        private int GetPrice()
        {
            if (GameManager.Instance.GameData.onBoarding)
            {
                return price;
            }
            return PlayerPrefs.GetInt(key,price);
        }

        private void Reset()
        {
            PlayerPrefs.DeleteKey(key);
        }

        public void Interact(GameObject interactor)
        {
            if(unlocked) return;
            if(!interactor.CompareTag("Player")) return;
            
            if(MoneySystem.Instance.playerMoney<1) return;
            //TODO:Refactor this into its own class 
            Transaction(interactor);
            Transaction(interactor);
            PlayerPrefs.SetInt(key,currentPrice);
        }

        private void Transaction(GameObject interactor)
        {
            if(unlocked) return;
            var money = Instantiate(MoneySystem.Instance.moneyPf, interactor.transform.position + Vector3.up,
                interactor.transform.rotation);
            var seq = DOTween.Sequence();
            money.transform.localScale = Vector3.zero;
            var duration = 0.4f;

            seq.Append(money.transform.DOMoveY(transform.position.y, duration));
            seq.Insert(0,
                money.transform.DOMoveX(transform.position.x, duration)
                    .SetEase(Random.Range(0, 1f) > 0.5f ? Ease.OutSine : Ease.InSine));
            seq.Insert(0,
                money.transform.DOMoveZ(transform.position.z, duration)
                    .SetEase(Random.Range(0, 1f) > 0.5f ? Ease.InQuad : Ease.OutQuad));
            seq.Insert(0, money.transform.DOScale(1, duration));
            // seq.Append(money.transform.DOPunchScale(Vector3.one*0.2f,0.3f));
            seq.onComplete += () => { Destroy(money.gameObject); };

            currentPrice -= 1;
            currentPrice = Mathf.Max(0, currentPrice);
            ValueChanged?.Invoke(Value);
            MoneySystem.Instance.SpendMoney(1);
            
            if (currentPrice > 0) return;
                Unlock();
        }
        

        public void Unlock()
        {
            onUnlock?.Invoke();
            unlocked = true;
            PlayerPrefs.SetInt(key,currentPrice);
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
                var itemRackPopulator = spawnedObject.GetComponent<ItemRackPopulator>();
                if (itemRackPopulator)
                {
                    itemRackPopulator.itemToPopulate = itemToPopulate;
                    itemRackPopulator.shouldPopulate = shouldPopulate;
                }
                var holder = spawnedObject.GetComponent<LimitedItemHolder>();
                FreeIsleProvider.Instance.Add(holder);
            }
        }
        public ProgressData Value => new()
            { percentage = 1 - (float)currentPrice / price, text = currentPrice.ToString() };
        public event Action<ProgressData> ValueChanged;
    }
}