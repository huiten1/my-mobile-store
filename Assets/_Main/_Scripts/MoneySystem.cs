using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public static MoneySystem Instance;

    [Header("UI")]
    public TMP_Text moneyText;

    [Header("Animation")]
    public Transform parentTf;
    public Transform targetTf;
    public GameObject moneyPf;

    public static int playerMoney;



    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerMoney = PlayerPrefs.GetInt("money");

        UpdateMoney();
    }

    // Add money to the player's total
    public void AddMoney(int amount)
    {
        playerMoney += amount;
        UpdateMoney();
    }

    // Subtract money from the player's total (if sufficient funds)
    public bool SpendMoney(int amount)
    {
        if (playerMoney >= amount)
        {
            playerMoney -= amount;
            UpdateMoney();
            return true; // Purchase successful
        }
        else
        {
            return false; // Insufficient funds
        }
    }

    // Update the UI Text component
    void UpdateMoney()
    {
        if (moneyText != null)
        {
            moneyText.text = playerMoney.ToString();
        }
        PlayerPrefs.SetInt("money", playerMoney);
    }
}
