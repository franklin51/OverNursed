using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBar : MonoBehaviour
{
    private Animator animator;

    public void completeAnimation(){
            animator = GetComponent<Animator>();
            animator.Play ("missionComplete");

    }

    public void failedAnimation(){
        animator = GetComponent<Animator>();
            animator.Play ("missionFailed");
    }
}
