using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace _Main._Scripts
{
    public class ItemStackHolder : ItemHolder
    {
        [SerializeField] private Transform holderTf;
        private List<GameObject> items = new List<GameObject>();
        public override bool IsFull => items.Count == capacity;
        public int capacity = 5;
        public override bool HasItems => items.Count > 0;
        
        public override void Add(GameObject item)
        {
            if(!item) return;
            if(IsFull) return;
            item.transform.SetParent(holderTf,false);
            items.Add(item);
            var targetLocalPos = new Vector3(0, items.Count - 1, 0);
            item.transform.DOLocalJump(targetLocalPos, 1f, 1, 0.6f);
        }
        public override GameObject Pop()
        {
            if (items.Count == 0) return null;
            var res = items.Last();
             items.RemoveAt(items.Count-1);
             return res;
        }

        void UpdateStack()
        {
            for (int i = 0; i < items.Count; i++)
            {
                var targetLocalPos = new Vector3(0, i, 0);
                items[i].transform.DOLocalMove(targetLocalPos, 0.6f);
            }
        }

        public override GameObject Get(string filter)
        {
            int count = items.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (items[i].name.ToLower().Contains(filter.ToLower()))
                {
                    var res = items[i];
                    items.RemoveAt(i);
                    UpdateStack();
                    return res;
                }
            }

            return null;
        }
    }
}