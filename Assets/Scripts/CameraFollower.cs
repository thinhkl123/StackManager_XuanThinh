using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smooth;
    Vector3 distance;
    // Start is called before the first frame update
    void Start()
    {
        distance = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.y >= 0)
        {
            Follow();
        }
    }

    void Follow()
    {
        Vector3 currentPosition = transform.position;

        Vector3 newPosiotion = target.position - distance;

        transform.position = Vector3.Lerp(currentPosition, newPosiotion, smooth * Time.deltaTime);
    }
}
