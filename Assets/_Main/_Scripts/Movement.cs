using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    public static Movement Instance;

    public bool isHold;
    public float moveSpeed;
    public float xInput, yInput;
    public FloatingJoystick floatingJoystick;
    public Animator animator;
    public GameObject segway;
    public ParticleSystem smokeExplosionWhite;

    public CharacterController cc;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isHold = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHold = false;
        }

        if (isHold)
        {
            xInput = floatingJoystick.Horizontal;
            yInput = floatingJoystick.Vertical;

            Vector3 direction = new Vector3(xInput, 0, yInput);
            
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            cc.transform.rotation = Quaternion.Euler(0,angle + 45f,0);
            
            if (floatingJoystick.Direction.magnitude>0.5f)
            {
                cc.Move(transform.forward * (moveSpeed * Time.deltaTime));
                var pos = transform.position;
                pos.y = 0;
                cc.transform.position = pos;
                // rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * rb.transform.forward);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!hit.rigidbody) return;
        hit.rigidbody.AddForceAtPosition(-hit.normal*10f,hit.point);
    }
}
