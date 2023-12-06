using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockArea : MonoBehaviour
{
    [Header("Unlock")]
    public bool isUnlocked;
    public int unlockCost;
    public List<Sprite> bgSprites = new List<Sprite>();
    public Collider unlockTrigger;
    private bool isStanding;
    public SpriteRenderer bgImage;
    public GameObject table;
    public GameObject cashier;
    public Canvas canvas;
    public TMP_Text unlockCostTxt;
    public Image fillImg;
    public int currentGivenMoney;

    [Header("Item purchase")]
    public GameObject itemPf;
    public Item.Type tableType;
    public List<Transform> itemPoints = new List<Transform>();
    public List<Transform> inspectPoints = new List<Transform>();

    Vector3 scale;


    private void Start()
    {
        if (unlockTrigger == null)
        {
            unlockTrigger = GetComponent<Collider>();
            if (!unlockTrigger.isTrigger) unlockTrigger.isTrigger = true;
        }

        if (canvas != null)
            canvas.renderMode = RenderMode.WorldSpace;

        bgImage.sprite = bgSprites[(int)tableType];
        fillImg.fillAmount = 0;
        currentGivenMoney = 0;
        scale = transform.lossyScale;
        unlockCostTxt.text = unlockCost.ToString();

        SpawnItems(tableType);
    }

    private void FixedUpdate()
    {
        if (isStanding)
        {
            if (!isUnlocked) Unlock();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isStanding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isStanding = false;
        }
    }

    void Unlock()
    {
        if (MoneySystem.Instance.playerMoney <= 0 || currentGivenMoney >= unlockCost) return;

        GameObject moneyTmp = Instantiate(MoneySystem.Instance.moneyPf);
        moneyTmp.transform.DOMove(Player.Instance.transform.position + Vector3.up * 2.5f, Random.Range(0.25f, 0.5f))
        .From(Player.Instance.transform.position).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            moneyTmp.transform.DOMove(transform.position, Random.Range(0.25f, 0.5f)).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                Destroy(moneyTmp);

                if (currentGivenMoney >= unlockCost)
                {
                    isUnlocked = true;
                    bgImage.gameObject.SetActive(false);
                    if (tableType == Item.Type.none) cashier.SetActive(true);
                    else if (tableType != Item.Type.none) table.SetActive(true);
                    canvas.gameObject.SetActive(false);
                    transform.DOScale(scale * 1.5f, 0.25f).From(scale).SetEase(Ease.InOutBounce).OnComplete(() =>
                    {
                        transform.DOScale(scale, 0.25f).SetEase(Ease.InOutBounce);
                    });
                }
                else
                {
                    isUnlocked = false;
                }
            });
        });

        int dropAmount = 1;
        currentGivenMoney += dropAmount;
        float one = unlockCost;
        float value = currentGivenMoney * one / (one * one);
        fillImg.DOFillAmount(value, 0.5f).OnComplete(() =>
        {
            fillImg.fillAmount = value;
        });
        unlockCostTxt.text = (unlockCost - currentGivenMoney).ToString();
        MoneySystem.Instance.SpendMoney(dropAmount);
    }

    void SpawnItems(Item.Type type)
    {
        for (int i = 0; i < itemPoints.Count; i++)
        {
            GameObject itemTmp = Instantiate(itemPf, itemPoints[i]);
            itemTmp.transform.eulerAngles = itemPoints[i].eulerAngles;
            itemTmp.GetComponent<Item>().itemType = type;
            itemTmp.GetComponent<Item>().SetType(type);
        }
    }

}
