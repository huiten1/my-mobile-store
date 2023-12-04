using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{
    public bool canMove;
    public Transform targetPos;
    public Vector3 offset;
    public float moveSpeed;
    public NavMeshAgent navMeshAgent;



    private void Start()
    {

    }

    private void Update()
    {
        if (!canMove) return;
        MoveToTarget(targetPos);
    }
    public void MoveToTarget(Transform target)
    {
        Vector3 targetPos = target.position + offset.x * target.right + offset.z * target.forward;
        navMeshAgent.SetDestination(targetPos);

        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.DORotate(new Vector3(0, angle, 0), 0.5f);
    }
}
