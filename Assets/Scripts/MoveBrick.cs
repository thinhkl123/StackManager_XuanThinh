using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBrick : BrickPlayer
{
    private void Awake()
    {
        isTake = false;
    }

    public Player.Direction firstDirection;
    public Player.Direction secondDirection;
}
