using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPlayer : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    public bool isTake;

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
        //brick.SetActive(false);
        //Destroy(brick);
    }

    public Vector3 GetFirstPosOfBrick()
    {
        return brick.transform.position;
    }

    public void SetBrickForPlayer(Vector3 pos, GameObject parent)
    {
        brick.transform.SetParent(parent.transform);
        brick.transform.localRotation = Quaternion.Euler(-90, 0, -180);
        brick.transform.position = pos;
    }

    public GameObject GetBrick()
    {
        return brick;
    }

    public void HideBrick()
    {
        brick.SetActive(false);
    }
}
