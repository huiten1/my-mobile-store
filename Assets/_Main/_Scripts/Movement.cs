using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    public bool isHold;
    public float moveSpeed;
    public float xInput, yInput;
    public FloatingJoystick floatingJoystick;
    public Animator animator;

    private void Start()
    {

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
            transform.DORotate(new Vector3(0, angle + 45f, 0), 0.5f);
            if (xInput > 0.5f || xInput < -0.5f || yInput > 0.5f || yInput < -0.5f)
            {
                transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
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
}
