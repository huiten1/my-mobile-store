using System.Collections.Generic;

namespace Lib.StateMachine
{
    public class StateMachine
    {
        private Dictionary<string,IState> stateDict = new();
        protected IState currentState;
        public IState CurrentState => currentState;
        public void AddState(IState state)
        {
            stateDict.Add(state.GetType().Name, state);
            state.ChangeState += SetState;
        }
        public void SetState(string stateName)
        {
            if (stateDict.TryGetValue(stateName, out var state))
            {
                SetState(state);
            }
        }
        public void SetState(IState state)
        {
            if(currentState!=null)
                currentState.Exit();
            currentState = state;
            if(currentState!=null)
                currentState.Enter();
        }

        public void Tick()
        {
            currentState?.Tick();
        }
    }
}