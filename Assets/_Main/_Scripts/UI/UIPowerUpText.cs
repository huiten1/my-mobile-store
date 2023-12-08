using DG.Tweening;
using Lib.GameEvent;
using TMPro;
using UnityEngine;

namespace _Game.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class UIPowerUpText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();

            Resources.Load<GameEventString>("Events/PowerUp/PowerUpText").AddListener(HandlePowerUpText);
        }

        private void HandlePowerUpText(string text)
        {
            var rect = transform as RectTransform;
            rect.anchoredPosition = new Vector2(0, -Screen.height);

            var seq = DOTween.Sequence();
            var duration = 0.6f;
            seq.Append(rect.DOAnchorPos(new Vector2(0,150), duration));
            seq.Insert(duration + 1.5f, rect.DOAnchorPos(new Vector2(0, Screen.height), duration));
            _text.SetText(text);
        }
    }
}