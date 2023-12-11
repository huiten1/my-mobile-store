using System;
using UnityEngine;

namespace _Main._Scripts.Spawner
{
    public class GridSpawner : Spawner
    {
        [SerializeField] private int width = 10;
        [SerializeField] private float widthOffset = 0.5f;
        [SerializeField] private int length = 10;
        [SerializeField] private float lengthOffset = 0.5f;
        [SerializeField] private int height = 10;
        [SerializeField] private float heightOffset = 0.5f;
        private GameObject[] spawnedObjectArray;
        public bool showGizmos;
        private void Awake()
        {
            spawnedObjectArray = new GameObject[width*length*height];
        }
        
        
        
        public override T[] Spawn<T>(T prefab, int count, Action<T> onSpawn)
        {
            T[] res = new T[count];
            int[] emptyIndexex = new int[count];
            int counter = 0;
            for (int i = 0; i < spawnedObjectArray.Length; i++)
            {
                if (spawnedObjectArray[i] == null)
                {
                    emptyIndexex[counter] = i;
                    counter++;
                    if (counter >= emptyIndexex.Length)
                    {
                        break;
                    }
                }
            }
            
            for (int i = 0; i < count; i++)
            {
                var targetIndex = emptyIndexex[i];

                int x = targetIndex / length  % width;
                int y = targetIndex / (width * height) ;
                int z = targetIndex % length;
                Vector3 spawnPoint = new Vector3(x * widthOffset,y * heightOffset,z*lengthOffset);
                var spawnedObject =Instantiate(prefab,transform.TransformPoint( spawnPoint), Quaternion.identity);
                onSpawn?.Invoke(spawnedObject);
                spawnedObjectArray[targetIndex] = spawnedObject.gameObject;
                res[i] = spawnedObject;
            }
            return res;
        }

        private void OnDrawGizmos()
        {
            if(!showGizmos) return;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < length; z++)
                    {
                        var center = new Vector3(x * widthOffset, y * heightOffset, z * lengthOffset) ;
                        var size = new Vector3(widthOffset, heightOffset, lengthOffset);
                        center -= size / 2f;
                        Gizmos.color = Color.blue;
                        Gizmos.DrawWireCube( 
                            transform.TransformPoint(center)
                            ,size);
                    }
                }
                
            }
        }
    }
    
}