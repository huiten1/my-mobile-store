using System;
using System.Collections.Generic;
using _Game.UI;
using _Main._Scripts.SaveLoad;
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
        [SerializeField] private GameObject[] objectsToDisable;
        [SerializeField] private GameObject[] objectsToEnable;
        private SequenceState currentState;
        private int currentStateIndex;
        private void Start()
        {
            bool hasOnboarding = SaveManager.Load<GameData>().onBoarding;


            foreach (var go in objectsToEnable)
            {
                go.SetActive(hasOnboarding);
            }
            
            foreach (var go in objectsToDisable)
            {
                go.SetActive(!hasOnboarding);
            }
            
            if(!hasOnboarding) return;

            currentState = sequenceStates[0];
            currentState.exitEvent.AddListener(NextState);
            currentState.Enter();
        }

        private void NextState()
        {
            sequenceStates[currentStateIndex].exitEvent.RemoveListener(NextState);
            currentStateIndex++;
            if (currentStateIndex >= sequenceStates.Count)
            {
                return;
            }
           
            currentState = sequenceStates[currentStateIndex];
            currentState.Enter();
            currentState.exitEvent.AddListener(NextState);
            ValueChanged?.Invoke(Value);
        }
        
        public string Value => currentState?.text ?? "";
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
            arrow.transform.position = objectToUnlock.transform.position + Vector3.up * 3;
        }
    }
    
    
}