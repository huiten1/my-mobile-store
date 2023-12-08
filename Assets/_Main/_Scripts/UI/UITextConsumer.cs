using TMPro;
using UnityEngine;

namespace _Game.UI
{
    public class UITextConsumer : UIIndicatorConsumer<string>
    {
        [SerializeField] private TMP_Text tmpText;
        protected override void ValueUpdated(string newValue)
        {
            tmpText.SetText(newValue);
        }
    }
}