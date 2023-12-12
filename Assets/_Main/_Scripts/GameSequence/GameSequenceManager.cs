using System;
using System.Collections.Generic;
using _Game.UI;
using Lib.GameEvent;
using Lib.StateMachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Main._Scripts.GameSequence
{
    public class GameSequenceManager : MonoBehaviour , IIndicator<string>
    {
        public List<SequenceState> sequenceStates= new ();

        private SequenceState currentState;
        private int currentStateIndex;
        private void Start()
        {
            foreach (var sequenceState in sequenceStates)
            {
                sequenceState.exitEvent.AddListener(NextState);
            }

            currentState = sequenceStates[0];
            currentState.Enter();
        }

        private void NextState()
        {
            if (currentStateIndex >= sequenceStates.Count)
            {
                return;
            }
            currentStateIndex++;
            currentState = sequenceStates[currentStateIndex];
            currentState.Enter();
            ValueChanged?.Invoke(Value);
        }
        
        public string Value => currentState.text;
        public event Action<string> ValueChanged;
    }

    [System.Serializable]
    public class SequenceState
    {
        public UnityEvent OnActivate;
        public GameEvent exitEvent;
        public GameObject objectToUnlock;
        public GameObject arrow;
        public string text;
        public void Enter()
        {
            OnActivate?.Invoke();
            if(!objectToUnlock) return;
            objectToUnlock.gameObject.SetActive(true);
            arrow.transform.position = objectToUnlock.transform.position + Vector3.up * 5;
        }
    }
    
    
}