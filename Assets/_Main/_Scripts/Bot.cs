using System.Collections;
using System.Collections.Generic;
using _Main._Scripts;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public ItemHolder itemHolder;
    void Start()
    {
        
    }
    void Update()
    {
        animator.SetBool("carrying",itemHolder.HasItems);
        animator.SetFloat("speed",agent.velocity.magnitude/agent.speed);
    }
}
