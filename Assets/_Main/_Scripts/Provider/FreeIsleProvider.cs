using System;
using System.Collections.Generic;
using System.Linq;
using _Main._Scripts.Interaction;
using _Main._Scripts.Money;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main._Scripts.Provider
{
    public class FreeIsleProvider : Provider
    {
        public static FreeIsleProvider Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void Add(LimitedItemHolder isle) => isles.Add(isle);
        [SerializeField] private List<LimitedItemHolder> isles = new();


        private int index = 0;
        public override GameObject Get()
        {
            var isle = isles
                .Where(e => e.HasItems && e.GetComponent<Transaction>().shouldGiveTo == "Customer")
                .OrderBy(e => Guid.NewGuid())
                .FirstOrDefault();

            if (!isle) return null;
            
            var interactionPoints = isle.GetComponentsInChildren<Interactable>();
            index++;
            index %= interactionPoints.Length;
            return interactionPoints[index].gameObject;
        }
    }
}