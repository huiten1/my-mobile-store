using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public enum Type
    {
        none = 0, iWatch = 1, iPhone = 2, iPad = 3, macBook = 4, iMac = 5, airPods = 6
    }

    public UnityEvent OnPickedUp;
    public UnityEvent OnPlaced;
    public Type itemType;
    private Transform _currentType;
    public int price;

    public Vector3 insideBoxRotation;
    public void SetType(Type type)
    {
        itemType = type;
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == (int)itemType);
            if (i != (int)itemType) continue;
            _currentType = transform.GetChild(i);
        }
    }
}
