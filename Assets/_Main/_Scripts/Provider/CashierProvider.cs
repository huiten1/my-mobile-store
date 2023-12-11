using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main._Scripts.Provider
{
    public class CashierProvider : Provider
    {
        public static CashierProvider Instance;
        public List<GameObject> cashiers = new();
        private void Awake()
        {
            Instance = this;
        }
        
        public override GameObject Get()
        {
            if (cashiers.Count == 0)
            {
                return null;
            }
            return cashiers[Random.Range(0, cashiers.Count)];
        }

        public void Add(GameObject spawnedObject)
        {
            cashiers.Add(spawnedObject);
        }
    }
}