using System;
using _Game.UI;
using UnityEngine;

namespace Lib
{
    public class VariableSO<T> : ScriptableObject ,IIndicator<T>
    {
        [SerializeField] private T value;
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                ValueChanged?.Invoke(this.value);
            }
        }
        public event Action<T> ValueChanged = delegate {  };
        
    }
}