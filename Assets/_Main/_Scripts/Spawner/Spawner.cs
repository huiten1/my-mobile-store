using System;
using UnityEngine;

namespace _Main._Scripts.Spawner
{
    public abstract class Spawner : MonoBehaviour
    {
        public abstract T[] Spawn<T>(T prefab, int count, Action<T> onSpawn) where T : Component;
    }
}