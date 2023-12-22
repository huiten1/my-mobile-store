using System;
using UnityEngine;

namespace _Main._Scripts
{
    public class ItemRackPopulator : MonoBehaviour
    {
        public GameObject itemToPopulate;
        public  bool shouldPopulate;
        [SerializeField] private LimitedItemHolder itemRack;
        [SerializeField] private float spawnPeriod;
        private float t;
        private void Start()
        {
            if(!shouldPopulate) return;
            for (int i = 0; i < itemRack.MaxItemCount; i++)
            {
                itemRack.Add(Instantiate(itemToPopulate));
            }
        }

        private void Update()
        {
            if(!shouldPopulate) return;
            if (t <= 0)
            {
                t = spawnPeriod;
                if (!itemRack.IsFull)
                {
                    itemRack.Add(Instantiate(itemToPopulate));
                }
                return;
            }
            t -= Time.deltaTime;
        }
    }
}