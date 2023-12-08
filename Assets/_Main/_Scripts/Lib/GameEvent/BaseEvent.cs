using System;
using UnityEngine;
using UnityEngine.Events;

namespace Lib.GameEvent
{
  
    public class BaseEvent<T> : ScriptableObject
    {
        private Action<T> _event;

        public void Invoke(T value)
        {
            _event?.Invoke(value);
        }
        
        public void AddListener(Action<T> action) => _event+=action;
        public void RemoveListener(Action<T> action) => _event-=(action);
        public void RemoveAllListeners() => _event = delegate(T obj) {  };
    }
}