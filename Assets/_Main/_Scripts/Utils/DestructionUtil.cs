using UnityEngine;

namespace _Main._Scripts.Utils
{
    [CreateAssetMenu(menuName = "Util/Destruction Util")]
    public class DestructionUtil: ScriptableObject
    {
        public void Destroy(GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }
    }
}