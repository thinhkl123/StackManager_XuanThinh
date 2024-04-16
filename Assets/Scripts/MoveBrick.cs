using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBrick : BrickPlayer
{ 
    public Player.Direction firstDirection;
    public Player.Direction secondDirection;
    public Player.Direction thirdDirection;
    public Player.Direction fourthDirection;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        isTake = false;
    }

    public void PlayAni()
    {
        animator.SetTrigger("Push");
    }
}
