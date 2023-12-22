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
                onTargetStateReached?.Invoke();
            }
            else
            {
                onTargetNotReached?.Invoke();
            }
        }

        protected virtual void OnReachedState()
        {
            
        }

        protected virtual void OnNotReachedState()
        {
            
        }
    }
}