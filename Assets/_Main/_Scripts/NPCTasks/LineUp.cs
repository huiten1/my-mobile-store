using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent;
using Unity.VisualScripting;
using UnityEngine;

namespace _Main._Scripts.NPCTasks
{
    public class LineUp : NavMeshMovement
    {
        public SharedGameObject cashierGo;
        private ItemMoneyTransaction cashier;
        private int index;
        public override void OnStart()
        {
            cashier = cashierGo.Value.GetComponent<ItemMoneyTransaction>();
            cashier.waitingCustomers.Add(gameObject);
            // Debug.Log(cashier.waitingCustomers.Count);
            index = cashier.waitingCustomers.Count;
            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            if (cashier.waitingCustomers.Count == 0) return TaskStatus.Failure;
            index = cashier.waitingCustomers.IndexOf(gameObject);
            SetDestination(cashier.customerInteractionPoint + cashier.transform.forward*index);
            return TaskStatus.Running;
        }
    }
}