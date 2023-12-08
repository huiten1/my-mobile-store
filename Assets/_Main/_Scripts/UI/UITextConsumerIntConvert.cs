using TMPro;
using UnityEngine;

namespace _Game.UI
{
    public class UITextConsumerIntConvert : UIIndicatorConsumer<int>
    {
        [SerializeField] private string format;
        [SerializeField] private TMP_Text text;
        protected override void ValueUpdated(int newValue)
        {
            text.SetText(newValue.ToString(format));
        }
    }
}