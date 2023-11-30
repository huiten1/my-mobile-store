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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoneySystem.Instance.AddMoney(moneyAmount);

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameManager.Instance.mainCanvas.transform as RectTransform, screenPosition, GameManager.Instance.mainCanvas.worldCamera, out canvasPosition);
            Debug.Log("Canvas Position: " + canvasPosition);
            GameObject moneySpriteTmp = Instantiate(moneySprite, GameManager.Instance.mainCanvas.transform);
            moneySpriteTmp.transform.position = canvasPosition;

            float duration = Random.Range(minDuration, maxDuration);
            transform.DOMove(MoneySystem.Instance.moneyText.transform.position, duration).SetEase(easeType).OnComplete(() =>
            {
                Destroy(gameObject, 0.1f);
            });

        }
    }

    void Animate()
    {

    }


}
