using System;
using _Main._Scripts.Interaction;
using UnityEngine;

namespace _Main._Scripts
{
    public class Door : MonoBehaviour,IInteractable
    {
        [SerializeField] private Animator animator;
        [SerializeField] float waitTime = 0.5f;

        private float t = 0;
        private bool open;
        public void Interact(GameObject interactor)
        {
            if (!open)
            {
                open = true;
                animator.SetBool("open",open);
            }
            t = 0;
        }

        private void Update()
        {
            if (t > waitTime)
            {
                if (open)
                {
                    open = false;
                    animator.SetBool("open",open);
                }
                return;
            }
            t += Time.deltaTime;
        }
    }
}