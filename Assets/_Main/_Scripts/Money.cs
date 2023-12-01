using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using DG.Tweening;

public class Money : MonoBehaviour
{
    public int moneyAmount;
    public GameObject moneySprite;

    [Space]
    [Header("Animation settings")]
    [SerializeField][Range(0.5f, 0.9f)] float minDuration;
    [SerializeField][Range(0.9f, 2f)] float maxDuration;
    [SerializeField] Ease easeType;
    private Vector2 canvasPosition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoneySystem.Instance.AddMoney(moneyAmount);

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            GameObject moneySpriteTmp = Instantiate(moneySprite, MoneySystem.Instance.parentTf);
            moneySpriteTmp.transform.position = screenPosition;
            Destroy(gameObject);

            float duration = Random.Range(minDuration, maxDuration);
            moneySpriteTmp.transform.DOMove(MoneySystem.Instance.targetTf.position, duration).SetEase(easeType).OnComplete(() =>
            {
                Destroy(moneySpriteTmp);
                MoneySystem.Instance.targetTf.DOScale(Vector3.one * 0.8f, 0.5f).From(Vector3.one * 1.2f).SetEase(Ease.InOutBounce).OnComplete(() =>
                {
                    MoneySystem.Instance.targetTf.localScale = Vector3.one;
                });
            });

        }
    }


}
