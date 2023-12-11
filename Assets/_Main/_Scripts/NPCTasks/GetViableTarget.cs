using _Main._Scripts.Provider;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _Main._Scripts.NPCTasks
{
    public class GetViableTarget : Action
    {
        public SharedGameObject target;
        
        public override TaskStatus OnUpdate()
        {
            var freeIsle = FreeIsleProvider.Instance.Get();
            if (freeIsle)
            {
                
                target.SetValue( freeIsle);
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}