using _Main._Scripts.Provider;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _Main._Scripts.NPCTasks
{
    public class GetViableCashier : Action
    {
        public SharedGameObject returnValue;

        public override TaskStatus OnUpdate()
        {
            var res = CashierProvider.Instance.Get();
            if (res)
            {
                returnValue.SetValue(res);
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}