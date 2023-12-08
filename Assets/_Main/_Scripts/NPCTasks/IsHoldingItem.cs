using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _Main._Scripts.NPCTasks
{
    public class IsHoldingItem : Conditional
    {
        public SharedGameObject owner;
        private ItemHolder itemHolder;
        public override void OnStart()
        {
            itemHolder = owner.Value.GetComponent<ItemHolder>();
        }

        public override TaskStatus OnUpdate()
        {
            return itemHolder.HasItems ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}