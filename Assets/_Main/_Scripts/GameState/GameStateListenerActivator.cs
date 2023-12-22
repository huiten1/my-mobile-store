using UnityEngine;

namespace _Main._Scripts.GameState
{
    public class GameStateListenerActivator : GameStateListener
    {
        protected override void OnReachedState()
        {
            base.OnReachedState();
            gameObject.SetActive(false);
        }

        protected override void OnNotReachedState()
        {
            base.OnNotReachedState();
            gameObject.SetActive(false);
        }
    }
}