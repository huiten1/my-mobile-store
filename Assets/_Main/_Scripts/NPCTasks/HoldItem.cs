using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _Main._Scripts.NPCTasks
{
    public class HoldItem : Action
    {
        public SharedGameObject owner;
        public SharedGameObject itemRack;
        private ItemHolder itemHolder;
        public override void OnStart()
        {
            itemHolder = owner.Value.GetComponent<ItemHolder>();
        }

        public override TaskStatus OnUpdate()
        {
            var item = itemRack.Value.GetComponent<ItemHolder>().Pop();
            if (!item)
            {
                return TaskStatus.Failure;
            }
            itemHolder.Add(item);
            return TaskStatus.Success;
        }
    }
}