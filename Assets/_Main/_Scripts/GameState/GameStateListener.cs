using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Main._Scripts.GameState
{
    public class GameStateListener : MonoBehaviour
    {
        [SerializeField] private GameManager.GameState stateToReact;
        [SerializeField] private UnityEvent onTargetStateReached;
        [SerializeField] private UnityEvent onTargetNotReached;
        private void Awake()
        {
            GameManager.Instance.OnGameStateChanged += ReactToState;
        }

        private void Start()
        {
            ReactToState(GameManager.Instance.CurrentState);
        }

        private void ReactToState(GameManager.GameState currentState)
        {
            if (stateToReact == currentState)
            {
                OnReachedState();
            }
            else
            {
                OnNotReachedState();   
            }
        }

        protected virtual void OnReachedState()
        {
            onTargetStateReached?.Invoke();
        }

        protected virtual void OnNotReachedState()
        {
            onTargetNotReached?.Invoke();
        }
    }
}