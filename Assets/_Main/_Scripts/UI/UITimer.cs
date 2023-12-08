using System;
using TMPro;
using UnityEngine;

namespace _Game.UI
{
    public class UITimer : UIIndicatorConsumer<float>
    {
        [SerializeField] private TMP_Text text;

        protected override void ValueUpdated(float seconds)
        {
            var span = new TimeSpan(0, 0, (int)seconds);
            text.SetText($"{(int)span.TotalMinutes}:{span.Seconds:00}");
        }
    }
}