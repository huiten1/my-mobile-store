using Unity.VisualScripting;
using UnityEngine;

namespace _Main._Scripts.Utils
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _instance;
        
        public static T Instance
        {
            get
            {
                if (_instance==null)
                {
                    _instance = FindObjectOfType<T>();
                }
                if (!_instance)
                {
                    _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
                return _instance;
            }
        }
    
    }

}