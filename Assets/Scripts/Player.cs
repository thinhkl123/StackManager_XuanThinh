using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player Instance
     {  get; private set; }

    public event EventHandler OnWinLevel;

    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask brickPlayerLayerMask;
    [SerializeField] private GameObject visualGameObject;
    [SerializeField] private GameObject brickPb;

    private Vector3 startPos, endPos;
    private Vector3 destination, offset;
    private List<GameObject> brickList;
    private bool isJustDown;
    private Vector3 initPos;
    private int brickCount;
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

    private Direction newDirection;

    private void Awake()
    {
        //lastDirection = Direction.None;
        destination = transform.position;
        //isFirstTime = true;
        Instance = this;
        isJustDown = false;
        brickList = new List<GameObject>();
        initPos = transform.position;
        brickCount = 0;
    }

    private void Start()
    {
        GameManager.Instance.OnNextLevel += GameManager_OnNextLevel;
    }

    private void GameManager_OnNextLevel(object sender, System.EventArgs e)
    {
        OnInit();   
    }

    private void OnInit()
    {
        transform.position = initPos;
        isJustDown = false;
        destination = transform.position;
        brickList = new List<GameObject>();
        brickCount = 0;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            //Debug.Log("Move");
            if (newDirection != Direction.None)
            {
                GetDestination(newDirection);
            }
            else
            {
                GetDestination(GetDirection());
            }
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
        if (collision.collider.CompareTag("PlayerBrick") || collision.collider.CompareTag("MoveBrick"))
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            ClearBrick();

            Invoke(nameof(WinLevel), 1f);
        }
    }

    private void WinLevel()
    {
        OnWinLevel?.Invoke(this, EventArgs.Empty);
    }

    private void AddBrick(Collider collider)
    {
        BrickPlayer playerBrick = collider.GetComponentInParent<BrickPlayer>();
        if (playerBrick != null)
        {
            if (!playerBrick.IsTake())
            {
                if (isJustDown)
                {
                    visualGameObject.transform.position += new Vector3(0, 0.45f, 0);
                    isJustDown = false;
                }
                Debug.Log("PlayerBrick");
                //GameObject brick = Instantiate(brickPb, visualGameObject.transform.position, brickPb.transform.rotation, this.transform);
                //brickList.Add(brick);
                brickCount++;
                if (brickCount > brickList.Count)
                {
                    GameObject brick = Instantiate(brickPb, visualGameObject.transform.position, brickPb.transform.rotation, this.transform);
                    brickList.Add(brick);
                }
                else
                {
                    brickList[brickCount - 1].SetActive(true);
                }
                visualGameObject.transform.position += new Vector3(0, 0.45f, 0);
                playerBrick.SetTake();
            }
        }
    }

    private void RemoveBrick(Bridge bridge)
    {
        /*
        GameObject brickTemp = brickList[brickList.Count - 1];
        brickList.RemoveAt(brickList.Count - 1);
        Destroy(brickTemp);
        */
        brickCount--;
        brickList[brickCount].SetActive(false);
        visualGameObject.transform.position += new Vector3(0, -0.45f, 0);
        bridge.SetPut();
    }


    private void ClearBrick()
    {
        int count = brickList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject brickTemp = brickList[brickList.Count - 1];
            brickList.RemoveAt(brickList.Count - 1);
            Destroy(brickTemp);
            visualGameObject.transform.position += new Vector3(0, -0.45f, 0);
        }
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

        newDirection = Direction.None;

        while (true)
        {
            RaycastHit hit;
            if (Physics.Raycast(destination + offset, Vector3.down, out hit, 5f, brickPlayerLayerMask))
            {
                //Debug.Log(hit.collider);
                destination += offset;
                if (hit.collider.CompareTag("MoveBrick"))
                {
                    MoveBrick moveBrick = hit.collider.GetComponentInParent<MoveBrick>();
                    if (moveBrick != null)
                    {
                        Debug.Log("MoveBrick");
                        if (direction == moveBrick.firstDirection)
                        {
                            newDirection = moveBrick.secondDirection;
                        }
                        else if (direction == moveBrick.secondDirection)
                        {
                            newDirection = moveBrick.firstDirection;
                        }
                        Debug.Log(newDirection);
                        break;
                    }
                }
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
            else if (theTouch.phase == TouchPhase.Ended /*|| theTouch.phase == TouchPhase.Moved*/)
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
