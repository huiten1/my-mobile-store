using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Lib.GameEvent
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game/Events/GameEvent", order = 0)]
    public class GameEvent : ScriptableObject
    {
        private List<Action> _eventListeners = new ();
        public void Invoke()
        {
            Debug.Log($"{name} has triggered {_eventListeners.Count} methods : {_eventListeners.Select(e=>e.Method.Name).Aggregate("",(sum,res)=>sum + ", " +res )}");

            for (int i = _eventListeners.Count - 1; i >= 0; i--)
            {
                _eventListeners[i]?.Invoke();
            }
        }

        public void AddListener(Action action)
        {
            Debug.Log($"Trying to add action:{action.Method.Name} to event {name}");
            if(_eventListeners.Contains(action)) return;
            Debug.Log($"Added action:{action.Method.Name}");
            _eventListeners.Add(action);
        }

        public void RemoveListener(Action action)
        {
            if (_eventListeners.Contains(action))
            {
                _eventListeners.Remove(action);
            }   
        }

        public void RemoveAllListeners() => _eventListeners.Clear();
    
    }
}