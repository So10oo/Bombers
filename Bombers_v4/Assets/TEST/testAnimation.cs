using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnimation : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            animator.SetTrigger("Heal");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("Invulnerable", true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("Invulnerable", false);
        }
    }
}
