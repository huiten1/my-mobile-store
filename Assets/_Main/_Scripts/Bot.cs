using System;
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
    public Transform itemHoldSlot;
    void Start()
    {

    }
    void Update()
    {
        animator.SetBool("carrying",itemHolder.HasItems);
        animator.SetFloat("speed",agent.velocity.magnitude/agent.speed);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        // if(layerIndex!=1) return;
        if (!itemHolder.HasItems)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand,Mathf.Lerp(animator.GetIKPositionWeight(AvatarIKGoal.RightHand),0f,Time.deltaTime*5));
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,Mathf.Lerp(animator.GetIKPositionWeight(AvatarIKGoal.LeftHand),0f,Time.deltaTime*5));
            return;
        }

        var bottomItem = itemHoldSlot.GetChild(0);
        var itemCollider = bottomItem.GetComponentInChildren<Collider>();
        
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1f);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1f);

        Vector3 offset = itemHoldSlot.TransformDirection(new Vector3(0, -0.3f, 0.2f));
        var center = itemCollider.bounds.center;
        var extents = itemCollider.bounds.extents;

        animator.SetIKPosition(AvatarIKGoal.RightHand, center + transform.TransformDirection( new Vector3(extents.x - 0.05f,-extents.y,0)) + offset);
        animator.SetIKPosition(AvatarIKGoal.LeftHand,center+ transform.TransformDirection(  new Vector3(-extents.x  + 0.05f,-extents.y,0)) + offset);
    }

    private void OnDrawGizmos()
    {
        // throw new NotImplementedException();
        Gizmos.DrawWireSphere(animator.GetBoneTransform(HumanBodyBones.RightHand).position,0.1f);
        Gizmos.color = Color.magenta;
        
        Gizmos.DrawWireSphere(animator.GetIKPosition(AvatarIKGoal.RightHand),0.1f);
    }
}
