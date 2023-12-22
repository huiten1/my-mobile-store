using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main._Scripts.Spawner
{
    public class PeriodicSpawner : MonoBehaviour
    {
        [SerializeField] private float period;
        [SerializeField] private Spawner spawner;
        [SerializeField] private GameObject[] prefabs;
        private float t;
        private int idx;

        private void Start()
        {
            period = GameManager.Instance.GameData.botSpawnTime;
        }

        private void Update()
        {
            if (t <=0)
            {
                t = period;
                Spawn();
            }

            t -= Time.deltaTime;
        }

        public void Spawn()
        {
            spawner.Spawn(prefabs[idx].transform, 1, null);
            idx++;
            idx %= prefabs.Length;
        }
    }
}