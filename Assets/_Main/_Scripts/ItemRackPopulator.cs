using System;
using UnityEngine;

namespace _Main._Scripts
{
    public class ItemRackPopulator : MonoBehaviour
    {
        [SerializeField] private GameObject itemToPopulate;

        [SerializeField] private LimitedItemHolder itemRack;

        private void Start()
        {
            for (int i = 0; i < itemRack.MaxItemCount; i++)
            {
                itemRack.Add(Instantiate(itemToPopulate));
            }
        }
    }
}