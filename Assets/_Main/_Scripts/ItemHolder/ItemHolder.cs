using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Main._Scripts
{
    public abstract class ItemHolder : MonoBehaviour
    {
        public abstract bool IsFull { get; }
        public abstract bool HasItems { get; }
        public abstract void Add(GameObject item);
        public abstract GameObject Get(string filter,GameObject interactor);
        public abstract GameObject Pop();
    }
}