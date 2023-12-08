using System.Collections;
using UnityEngine;

namespace _Game.UI
{
    public abstract class UIIndicatorConsumer<T> : MonoBehaviour
    {
        protected IIndicator<T> Indicator;
        public GameObject indicatorObject;
        [Header("OR")]
        public ScriptableObject indicatorScriptableObject;
        private IEnumerator Start()
        {
            if (indicatorScriptableObject && indicatorScriptableObject is IIndicator<T>)
            {
                Indicator = indicatorScriptableObject as IIndicator<T>;
            }
            else if(indicatorObject)
            { 
                Indicator = indicatorObject.GetComponent<IIndicator<T>>();
            }
            if(Indicator==null) yield break;
            Indicator.ValueChanged += ValueUpdated;
            yield return null;
            ValueUpdated(Indicator.Value);
        }

        public void Setup(IIndicator<T> indicator)
        {
            if (Indicator != null)
            {
                Indicator.ValueChanged -= ValueUpdated;
            }
            Indicator = indicator;
            Indicator.ValueChanged += ValueUpdated;
            ValueUpdated(Indicator.Value);
            
        }

        public void Setup(GameObject indicatorObject)
        {
            Setup(indicatorObject.GetComponent<IIndicator<T>>());
         
        }
        protected abstract void ValueUpdated(T newValue);

        private void OnValidate()
        {
            if(!indicatorObject) return;
            if(indicatorObject.GetComponent<IIndicator<T>>()!=null) return;
            indicatorObject = null;
        }
    }
}