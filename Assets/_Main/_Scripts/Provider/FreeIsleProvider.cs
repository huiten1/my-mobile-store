using System;
using System.Collections.Generic;
using System.Linq;
using _Main._Scripts.Interaction;
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
        public override GameObject Get()
        {
            var isle = isles
                .Where(e => e.HasItems)
                .OrderBy(e => Guid.NewGuid())
                .FirstOrDefault();

            if (!isle) return null;
            var interactionPoints = isle.GetComponentsInChildren<Interactable>();
            return interactionPoints[Random.Range(0, interactionPoints.Length)].gameObject;
        }
    }
}