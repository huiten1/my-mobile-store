using System;

namespace Lib.StateMachine
{
    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();

        event Action<string> ChangeState;
    }
}