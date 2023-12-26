using System;
using UnityEngine;
using UnityEngine.Pool;

namespace _Main._Scripts.Spawner
{
    public class PointSpawner : Spawner
    {
        [SerializeField] private bool pooled;

        private IObjectPool<GameObject> objectPool;
        public override T[] Spawn<T>(T prefab, int count, Action<T> onSpawn)
        {
            T[] res = new T[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = Instantiate(prefab, transform.position, prefab.transform.rotation);
                onSpawn?.Invoke(res[i]);
            }

            return res;
        }
    }
}