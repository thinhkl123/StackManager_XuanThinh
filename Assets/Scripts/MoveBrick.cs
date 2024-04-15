using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBrick : BrickPlayer
{ 
    public Player.Direction firstDirection;
    public Player.Direction secondDirection;
    public Player.Direction thirdDirection;
    public Player.Direction fourthDirection;

    private void Awake()
    {
        isTake = false;
    }
}
