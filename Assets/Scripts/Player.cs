using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player Instance
     {  get; private set; }

    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask brickPlayerLayerMask;
    [SerializeField] private GameObject visualGameObject;
    [SerializeField] private GameObject brickPb;

    private Vector3 startPos, endPos;
    private Vector3 destination, offset;
    private List<GameObject> brickList;
    //private bool isFirstTime;
    RaycastHit hit;

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        None
    }

    //private Direction lastDirection;

    private void Awake()
    {
        //lastDirection = Direction.None;
        destination = transform.position;
        //isFirstTime = true;
        Instance = this;
        brickList = new List<GameObject>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            //Debug.Log("Move");
            GetDestination(GetDirection());
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        /*
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 5f, brickPlayerLayerMask))
        {
            Debug.Log("Add Brick");
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("PlayerBrick"))
            {
                PlayerBrick playerBrick = hit.collider.gameObject.GetComponentInParent<PlayerBrick>();
                if (playerBrick != null)
                {
                    Debug.Log("PlayerBrick");
                    if (!playerBrick.IsTake())
                    {
                        if (!isFirstTime)
                        {
                            Instantiate(brickPb, visualGameObject.transform.position, brickPb.transform.rotation, this.transform);
                            visualGameObject.transform.position += new Vector3(0, 0.5f, 0);
                            playerBrick.SetTake();
                        }
                        else
                        {
                            playerBrick.SetTake();
                            Instantiate(brickPb, visualGameObject.transform.position + new Vector3(0, -0.5f, 0), brickPb.transform.rotation, this.transform);
                            isFirstTime = false;
                        }
                    }
                }
            }
        }
        */
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerBrick"))
        {
            Debug.Log("AddBrick");
            AddBrick(collision.collider);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        Bridge bridge = other.GetComponentInParent<Bridge>();
        if (bridge != null)
        {
            if (!bridge.IsPut())
            {
                Debug.Log("RemoveBrick");
                RemoveBrick(bridge);
            }
        }
        
    }

    private void AddBrick(Collider collider)
    {
        BrickPlayer playerBrick = collider.GetComponentInParent<BrickPlayer>();
        if (playerBrick != null)
        {
            if (!playerBrick.IsTake())
            {
                Debug.Log("PlayerBrick");
                GameObject brick = Instantiate(brickPb, visualGameObject.transform.position, brickPb.transform.rotation, this.transform);
                brickList.Add(brick);
                visualGameObject.transform.position += new Vector3(0, 0.45f, 0);
                playerBrick.SetTake();
            }
        }
    }

    private void RemoveBrick(Bridge bridge)
    {
        Destroy(brickList[brickList.Count - 1]);
        visualGameObject.transform.position += new Vector3(0, -0.45f, 0);
        bridge.SetPut();
    }

    private void GetDestination(Direction direction)
    {
        //Debug.Log(direction);
        if (direction == Direction.None)
        {
            return;
        }

        switch (direction)
        {
            case Direction.Left:
                offset = new Vector3(-1, 0, 0);
                break;
            case Direction.Right:
                offset = new Vector3(1, 0, 0);
                break;
            case Direction.Up:
                offset = new Vector3(0, 0, 1);
                break;
            case Direction.Down:
                offset = new Vector3(0, 0, -1);
                break;
        }

        while (true)
        {
            RaycastHit hit;
            if (Physics.Raycast(destination + offset, Vector3.down, out hit, 5f, brickPlayerLayerMask))
            {
                //Debug.Log(hit.collider);
                destination += offset;
            }
            else
            {
                break;
            }
        }

        Debug.Log(destination + " " + direction);
    }

    private Direction GetDirection()
    {
        Direction dir = Direction.None;
        if (Input.touchCount > 0)
        {
            Touch theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                startPos = theTouch.position;
            }
            else if (theTouch.phase == TouchPhase.Ended || theTouch.phase == TouchPhase.Moved)
            {
                endPos = theTouch.position;

                float x = endPos.x - startPos.x;
                float y = endPos.y - startPos.y;

                if (Mathf.Abs(x) <= 0.1 && Mathf.Abs(y) <= 0.1)
                {
                    dir = Direction.None;
                }
                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    dir = x > 0 ? Direction.Right : Direction.Left;
                }
                else
                {
                    dir = y > 0 ? Direction.Up : Direction.Down;
                }
            }
        }

        return dir;
    }

    /*
    private void Move(Direction direction)
    {
        if (direction == Direction.None)
        {
            direction = lastDirection;
        }

        if (direction == Direction.None)
        {
            return;
        }

        if (direction == Direction.Left)
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }
        else if (direction == Direction.Right)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else if (direction == Direction.Up)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime);
        }
        else if (direction == Direction.Down)
        {
            transform.Translate(-transform.forward * speed * Time.deltaTime);
        }
    }
    */
}
