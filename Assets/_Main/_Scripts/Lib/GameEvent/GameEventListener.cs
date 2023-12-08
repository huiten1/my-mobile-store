using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Lib.GameEvent
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        [FormerlySerializedAs("OnInvoke")] public UnityEvent onTrigger;
        private void Awake()
        {
            gameEvent.AddListener(onTrigger.Invoke);
        }

        private void OnDestroy()
        {
            gameEvent.RemoveListener(onTrigger.Invoke);
        }
    }
}