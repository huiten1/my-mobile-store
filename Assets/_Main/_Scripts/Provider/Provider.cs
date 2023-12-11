using UnityEngine;

namespace _Main._Scripts.Provider
{
    public abstract class Provider : MonoBehaviour
    {
        public abstract GameObject Get();
    }
}