using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UnlockArea : MonoBehaviour
{
    public bool isUnlocked;
    public int unlockCost;
    public Collider boxTrigger;
    public GameObject image;
    public GameObject table;
    public List<Transform> points = new List<Transform>();
    public Canvas canvas;
    public Image fillImg;
    private int currentGivenMoney;


    private void Start()
    {
        if (boxTrigger == null)
        {
            boxTrigger = GetComponent<Collider>();
            if (!boxTrigger.isTrigger) boxTrigger.isTrigger = true;
        }

        if (canvas != null)
            canvas.renderMode = RenderMode.WorldSpace;

        fillImg.fillAmount = 0;
        currentGivenMoney = 0;
        // Debug.Log("REMAPPED VALUE " + Remap(5, 1, 10, 0, 1));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isUnlocked) Unlock();
        }
    }

    void Unlock()
    {
        if (MoneySystem.Instance.playerMoney <= 0) return;
        int dropAmount = 1;
        MoneySystem.Instance.SpendMoney(dropAmount);
        currentGivenMoney += dropAmount;
        float remappedValue = Remap(currentGivenMoney, 1, 10, 0, 1);
        Debug.Log(remappedValue);
        float one = unlockCost / 100;
        float value = one * currentGivenMoney;
        fillImg.fillAmount = value / 100;

        Animate();

        if (currentGivenMoney >= unlockCost)
        {
            isUnlocked = true;
            image.SetActive(false);
            table.SetActive(true);
            canvas.gameObject.SetActive(false);
        }
        else
        {
            isUnlocked = false;
        }
    }

    float Remap(float value, float low1, float high1, float low2, float high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }

    void Animate()
    {
        GameObject moneyTmp = Instantiate(MoneySystem.Instance.moneyPf);
        moneyTmp.transform.DOMove(Player.Instance.transform.position + Vector3.up * 2.5f, Random.Range(0.25f, 0.5f))
        .From(Player.Instance.transform.position).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            moneyTmp.transform.DOMove(transform.position, Random.Range(0.25f, 0.5f)).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                Destroy(moneyTmp);
            });
        });
    }

}
