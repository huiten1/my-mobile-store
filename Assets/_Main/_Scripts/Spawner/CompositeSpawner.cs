using System;
using System.Linq;
using UnityEngine;

namespace _Main._Scripts.Spawner
{
    public class CompositeSpawner : Spawner
    {
        [SerializeField] private bool nearestToPlayer;
        private int currentIndex;
        [SerializeField] private Spawner[] spawners;
        public override T[] Spawn<T>(T prefab, int count, Action<T> onSpawn)
        {
            T[] res = new T[count];
            for (int i = 0; i < count; i++)
            {
                Spawner spawner;
                if (nearestToPlayer)
                {
                    spawner = spawners.Where(e =>
                            {
                                var viewPortPoint = Camera.main.WorldToViewportPoint(e.transform.position);
                                return Mathf.Abs(viewPortPoint.x) > 1f || Mathf.Abs(viewPortPoint.y) > 1f;
                            }
                         )
                        .OrderBy(e =>
                            Vector3.Distance(e.transform.position, Player.Instance.transform.position))
                        .FirstOrDefault();
                    if (spawner == null)
                    {
                        spawner = spawners[currentIndex];
                    }
                }
                else
                {
                    spawner = spawners[currentIndex];
                    currentIndex++;
                    currentIndex %= spawners.Length;
                    
                }
                
                res[i] = spawner.Spawn(prefab, 1, onSpawn)[0];

              
            }

            return res;
        }
    }
}