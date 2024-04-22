using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    //[SerializeField] private GameObject brick;
    [SerializeField] private Transform brickTransform;
    private bool isPut;

    private void Awake()
    {
        isPut = false;
        //brick.SetActive(false);
    }

    public bool IsPut()
    {
        return isPut;
    }

    public void SetPut()
    {
        isPut = true;
        //brick.SetActive(true);
    }

    public Vector3 GetBrickPos()
    {
        return brickTransform.position;
    }
}
