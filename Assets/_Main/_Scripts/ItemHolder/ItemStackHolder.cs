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
        public bool shouldAdd = true;

        private float stackHeight = 0;
        public override void Add(GameObject item)
        {
            if(!shouldAdd) return;
            if(!item) return;
            if(IsFull) return;
            item.transform.SetParent(holderTf,true);
            items.Add(item);
            
            item.GetComponent<Item>()?.OnPickedUp?.Invoke();
            
            

            var itemBound =  item.GetComponentInChildren<Collider>().bounds;
            var targetLocalPos = new Vector3(0, stackHeight, 0);
            stackHeight += itemBound.size.y;
            item.transform.DOLocalRotate(Vector3.zero, 0.6f);
            item.transform.DOLocalJump(targetLocalPos, 1f, 1, 0.6f).SetEase(Ease.OutQuad);
        }
        public override GameObject Pop()
        {
            if (items.Count == 0) return null;
            var res = items.Last();
            stackHeight -= res.GetComponentInChildren<Collider>().bounds.size.y;
            items.RemoveAt(items.Count-1);
             return res;
        }

        void UpdateStack()
        {
            float height = 0;
            for (int i = 0; i < items.Count; i++)
            {
                float sizeY =items[i].GetComponentInChildren<Collider>().bounds.size.y;
                var targetLocalPos = new Vector3(0, height, 0);
                height += sizeY;
                items[i].transform.DOLocalMove(targetLocalPos, 0.6f);
            }
        }

        public override GameObject Get(string filter,GameObject interactor)
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