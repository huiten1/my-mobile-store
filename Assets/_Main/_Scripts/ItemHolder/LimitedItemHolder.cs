using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace _Main._Scripts
{
    public class LimitedItemHolder : ItemHolder
    {
        [SerializeField] private Transform[] itemPositionTfs;
        public int MaxItemCount => itemPositionTfs.Length;
        [Serializable]
        class ItemSlot
        {
            public Transform slotTf;
            public GameObject item;
            public bool IsEmpty => item==null;
        }

        public UnityEvent onFull;
        public UnityEvent onNotEmpty;
        public override bool IsFull => itemSlots.All(e => !e.IsEmpty);
        public override bool HasItems => itemSlots.Any(e => !e.IsEmpty);
        private ItemSlot[] itemSlots;
        private void Awake()
        {
            SetupItemSlots();
        }

        private void SetupItemSlots()
        {
            itemSlots = new ItemSlot[itemPositionTfs.Length];
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i] = new ItemSlot()
                {
                    slotTf = itemPositionTfs[i]
                };
                
            }
        }

        public override void Add(GameObject item)
        {
            if (!item) return;
            if (!HasItems)
            {
                onNotEmpty?.Invoke();
            }
            foreach (var itemSlot in itemSlots)
            {
                if (itemSlot.IsEmpty)
                {
                    itemSlot.item = item;
                    item.transform.SetParent(itemSlot.slotTf, item.transform.parent);
                    var duration = .6f;
                    item.transform.DOLocalJump(Vector3.zero, 1.5f, 1, duration);
                    item.transform.DOLocalRotate(Vector3.zero, duration);
                    
                    item.GetComponent<Item>()?.OnPlaced?.Invoke();

                    if (IsFull)
                    {
                        onFull?.Invoke();
                    }
                    return;
                }
            }
        }   

        public override GameObject Pop()
        {
            foreach (var itemSlot in itemSlots)
            {
                if (!itemSlot.IsEmpty)
                {
                    var res = itemSlot.item;
                    itemSlot.item = null;
                    return res;
                }
            }
            return null;
        }

        public override GameObject Get(string filter,GameObject interactor)
        {
            var targetItem = itemSlots
                .Where(e => !e.IsEmpty && e.item.name.ToLower().Contains(filter.ToLower()))
                .OrderBy(e => Vector3.Distance(interactor.transform.position, e.item.transform.position))
                .FirstOrDefault();

            if (targetItem == null) return null;
            var res = targetItem.item;
            targetItem.item = null;
            return res;
        }
    }
}