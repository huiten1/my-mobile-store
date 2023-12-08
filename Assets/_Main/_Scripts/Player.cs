using System;
using System.Collections;
using System.Collections.Generic;
using _Main._Scripts;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private Animator animator;

    [SerializeField] private ItemHolder itemHolder;
    private static readonly int IsCarrying = Animator.StringToHash("isCarrying");

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        animator.SetBool(IsCarrying,itemHolder.HasItems); 
    }
}
