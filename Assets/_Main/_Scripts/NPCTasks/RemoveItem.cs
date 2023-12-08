using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _Main._Scripts.NPCTasks
{
    public class RemoveItem : Action
    {
        public SharedGameObject owner;
        private ItemHolder itemHolder;
        public SharedGameObject removedItem;
        public override void OnStart()
        {
            itemHolder = owner.Value.GetComponent<ItemHolder>();
        }

        public override TaskStatus OnUpdate()
        {
            removedItem = itemHolder.Pop();
            return TaskStatus.Success;
        }
    }
}