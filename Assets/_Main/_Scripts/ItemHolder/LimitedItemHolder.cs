using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

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
            if(!item) return;
           
            foreach (var itemSlot in itemSlots)
            {
                if (itemSlot.IsEmpty)
                {
                    itemSlot.item = item;
                    item.transform.SetParent(itemSlot.slotTf,false);
                    item.transform.DOLocalJump(Vector3.zero, 1f, 1, 0.6f);
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

        public override GameObject Get(string filter)
        {
            foreach (var itemSlot in itemSlots)
            {
                if (!itemSlot.IsEmpty && itemSlot.item.name.ToLower().Contains(filter.ToLower()))
                {
                    var res = itemSlot.item;
                    itemSlot.item = null;
                    return res;
                }
            }
            return null;
        }
    }
}