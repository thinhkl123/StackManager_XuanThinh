using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPlayer : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    private bool isTake;

    private void Awake()
    {
        isTake = false;
    }

    public bool IsTake()
    {
        return isTake;
    }

    public void SetTake()
    {
        isTake = true;
        brick.SetActive(false);
        Destroy(brick);
    }
}
