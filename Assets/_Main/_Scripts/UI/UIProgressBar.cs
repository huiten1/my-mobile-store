using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI
{
    public class UIProgressBar : UIIndicatorConsumer<ProgressData>
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _textMeshPro;
        protected override void ValueUpdated(ProgressData newValue)
        {
            _slider.value = newValue.percentage;
            _textMeshPro?.SetText(newValue.text);
        }
    }

    public struct ProgressData
    {
        public float percentage;
        public string text;
    }
}