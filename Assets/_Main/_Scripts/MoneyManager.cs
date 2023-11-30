using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [Header("UI")]
    [SerializeField] Canvas mainCanvas;
    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] GameObject moneyPrefab;
    [SerializeField] GameObject moneySprite;
    [SerializeField] Transform targetTf;

    [Space]
    [Header("Pooling")]
    [SerializeField] int maxCount;
    Queue<GameObject> moneyQueue = new Queue<GameObject>();

    [Space]
    [Header("Animation settings")]
    [SerializeField][Range(0.5f, 0.9f)] float mixDuration;
    [SerializeField][Range(0.9f, 2f)] float maxDuration;
    [SerializeField] Ease easeType;

    Vector3 targetPosition;



    private void Awake()
    {
        Instance = this;

        targetPosition = targetTf.position;

        // Prepare pooling
        PreparePool();
    }

    void PreparePool()
    {
        GameObject money;
        for (int i = 0; i < maxCount; i++)
        {
            money = Instantiate(moneyPrefab);
            money.transform.parent = transform;
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(money.transform.position);
            money.SetActive(false);
            moneyQueue.Enqueue(money);
        }
    }

    public void AddMoney(Vector3 collectedMoneyPosition, int amount)
    {
        MoneySystem.Instance.AddMoney(amount);
        // Animate(collectedMoneyPosition, amount);
    }

    void Animate(Vector3 collectedMoneyPosition, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (moneyQueue.Count > 0)
            {
                // GameObject money = moneyQueue.Dequeue();
                // money.SetActive(true);

                //
                // Vector3 viewportPosition = Camera.main.WorldToViewportPoint(collectedMoneyPosition);
                // Vector2 canvasPosition = new Vector2(
                // (viewportPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f),
                // (viewportPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f));
                // money.transform.position = canvasPosition;

                //
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
                Vector2 canvasPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvas.transform as RectTransform, screenPosition, mainCanvas.worldCamera, out canvasPosition);
                Debug.Log("Canvas Position: " + canvasPosition);
                GameObject moneySpriteTmp = Instantiate(moneySprite, mainCanvas.transform);
                moneySpriteTmp.transform.position = canvasPosition;

                float duration = Random.Range(mixDuration, maxDuration);
                transform.DOMove(targetPosition, duration).SetEase(easeType).OnComplete(() =>
                {

                });
            }
        }

    }

    // public static void WorldToCanvasPoint(this RectTransform rect, Vector3 worldPoint, Camera camera)
    // {
    //     Vector2 canvasSize = rect.sizeDelta;
    //     Vector3 screenPoint = camera.WorldToViewportPoint(worldPoint);
    //     Vector2 screenPoint2D = new Vector2(screenPoint.x, screenPoint.y);

    //     float canvasPointX = screenPoint2D.x * canvasSize.x - 0.5f * canvasSize.x;
    //     float canvasPointY = screenPoint2D.y * canvasSize.y - 0.5f * canvasSize.y;

    //     rect.anchoredPosition = new Vector2(canvasPointX, canvasPointY);
    // }

    // public void SetPositionTo(Vector3 position)
    // {
    //     WorldToCanvasPoint(canvasRectTransform, position, Camera.main);
    // }


}