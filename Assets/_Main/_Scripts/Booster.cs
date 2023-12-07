using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public enum BoosterType
    {
        speed
    }

    public BoosterType boosterType;
    public Collider trigger;
    public float speedBoostAmount;
    public float duration;
    private float currentSpeed;
    private bool isActive;
    private float elapsedTime;



    private void Update()
    {
        if (isActive)
        {
            elapsedTime += Time.deltaTime;
            if (duration <= elapsedTime)
            {
                Deactivate();
                elapsedTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isActive) Activate();
        }
    }

    public void Activate()
    {
        Movement.Instance.smokeExplosionWhite.Play();
        Movement.Instance.segway.SetActive(true);
        Movement.Instance.animator.SetBool("isRiding", true);
        currentSpeed = Movement.Instance.moveSpeed;
        Movement.Instance.moveSpeed = currentSpeed + speedBoostAmount;
        isActive = true;
    }

    public void Deactivate()
    {
        Movement.Instance.smokeExplosionWhite.Play();
        Movement.Instance.segway.SetActive(false);
        Movement.Instance.animator.SetBool("isRiding", false);
        Movement.Instance.moveSpeed = currentSpeed;
        isActive = false;
    }




}
