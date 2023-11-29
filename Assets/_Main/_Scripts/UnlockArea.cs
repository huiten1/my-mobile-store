using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockArea : MonoBehaviour
{
    public Collider boxTrigger;
    public GameObject image;
    public GameObject table;
    public List<Transform> points = new List<Transform>();


    private void Start()
    {
        if (boxTrigger == null)
        {
            boxTrigger = GetComponent<Collider>();
            if (!boxTrigger.isTrigger)
                boxTrigger.isTrigger = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            image.SetActive(false);
            table.SetActive(true);
        }
    }

}
