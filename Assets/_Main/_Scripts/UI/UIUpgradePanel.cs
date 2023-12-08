using System;
using Lib;
using Lib.GameEvent;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game.UI
{
    public class UIUpgradePanel : MonoBehaviour
    {
        [SerializeField] private int maxLevel;
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text lvlText;
        [SerializeField] private Image content;
        [SerializeField] private Sprite[] levelToSprite;
        [SerializeField] private IntVariable levelVariable;
        [SerializeField] private IntVariable costVariable;

        [FormerlySerializedAs("OnCanAfford")] [SerializeField] private UnityEvent<bool> onCantAfford;
        
        private Color startColor;
        private bool reachedMaxLevel;
        private void Start()
        {
            var playerGold = Resources.Load<IntVariable>("Data/PlayerGold");
            GetComponent<Button>().onClick.AddListener(OnClickBuy);
            ReactToPlayerGold(playerGold.Value);
            ReactToCost(costVariable.Value);
            ReactToLevel(levelVariable.Value);
            playerGold.ValueChanged += ReactToPlayerGold;
            costVariable.ValueChanged += ReactToCost;

            startColor = moneyText.color;
        }

        private void OnDestroy()
        {
            Resources.Load<IntVariable>("Data/PlayerGold").ValueChanged -= ReactToPlayerGold;
            costVariable.ValueChanged -= ReactToCost;
        }

        private void ReactToLevel(int level)
        {
            reachedMaxLevel = level == maxLevel;

            if (levelToSprite.Length > 0)
            {
                content.sprite = levelToSprite[Mathf.Clamp(level-1, 0, levelToSprite.Length - 1)];
            }

            if (lvlText)
            {
                lvlText.SetText($"Lvl {level}");
            }
            if (reachedMaxLevel)
            {
                lvlText.SetText("");
                onCantAfford?.Invoke(false);
                moneyText.SetText("MAX");
            }
            
        }
        private void ReactToCost(int cost)
        {
            moneyText.SetText(cost.ToString());
        }
        private void ReactToPlayerGold(int gold)
        {
            bool affordable = gold >= costVariable.Value;
            GetComponent<Button>().interactable = !reachedMaxLevel && affordable;
            onCantAfford?.Invoke(!affordable);
            // moneyText.colo = affordable ? startColor : Color.red;
            // transform.GetChild()
        }

        public void OnClickBuy()
        {
            Resources.Load<GameEventInt>("Events/Upgrade/GoldSpent").Invoke(costVariable.Value);
            levelVariable.Value += 1;
            ReactToLevel(levelVariable.Value);
        }
    }
}