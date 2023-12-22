using System;
using System.Collections;
using System.Collections.Generic;
using Lib.GameEvent;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public enum BoosterType
    {
        speed
    }
    public BoosterType boosterType;
    public float speedBoostAmount;
    public float duration;
    private float currentSpeed;
    private bool isActive;
    private float elapsedTime;

    [SerializeField] private GameEvent boostEvent;

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

    private IEnumerator Start()
    {
        speedBoostAmount = GameManager.Instance.GameData.scooterBonusSpeed;
        yield return null;
        Debug.Log("Trying to add listner");
        boostEvent.AddListener(Activate);
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
