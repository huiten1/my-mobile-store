﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Main._Scripts
{
    public abstract class ItemHolder : MonoBehaviour
    {
        public abstract bool IsFull { get; }
        public abstract bool HasItems { get; }
        public abstract void Add(GameObject item);
        public abstract GameObject Pop();

        public abstract GameObject Get(string filter);
        // public abstract GameObject FindItemByType
        public virtual void GiveItem(ItemHolder holder)
        {
            holder.Add(Pop());
        }
        public virtual void GiveItem(GameObject holder)
        {
            if (holder.GetComponent<ItemHolder>())
            {
                GiveItem(holder.GetComponent<ItemHolder>());
            }
        }
    }
}