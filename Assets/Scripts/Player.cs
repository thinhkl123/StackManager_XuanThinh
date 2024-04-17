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
    public event EventHandler OnWin;

    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask brickPlayerLayerMask;
    [SerializeField] private GameObject visualGameObject;
    [SerializeField] private GameObject brickPb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject tutorialUI;

    private Vector3 startPos, endPos;
    private Vector3 destination, offset;
    private List<GameObject> brickList;
    private bool isFirstTake;
    private Vector3 initPos;
    private float initPosY;
    private int brickCount;
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
    private MoveBrick curMoveBrick;

    private void Awake()
    {
        destination = transform.position;
        Instance = this;
        brickList = new List<GameObject>();
        initPos = transform.position;
        initPosY = transform.position.y;
        brickCount = 0;
        isFirstTake = true;
        curMoveBrick = null;
        newDirection = Direction.None;
    }

    private void Start()
    {
        GameManager.Instance.OnLoadLevel += GameManager_OnNextLevel;
    }

    private void GameManager_OnNextLevel(object sender, System.EventArgs e)
    {
        OnInit();   
    }

    private void OnInit()
    {
        transform.position = initPos;
        destination = transform.position;
        brickList = new List<GameObject>();
        brickCount = 0;
        isFirstTake = true;
        curMoveBrick = null;
        newDirection = Direction.None;
        animator.SetInteger("renwu", 0);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            //Debug.Log("Move");
            if (newDirection != Direction.None)
            {
                if (curMoveBrick != null)
                {
                    curMoveBrick.PlayAni();
                }
                GetDestination(newDirection);
            }
            else
            {
                GetDestination(GetDirection());
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerBrick") || collision.collider.CompareTag("MoveBrick"))
        {
            //Debug.Log("AddBrick");
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
                //Debug.Log("RemoveBrick");
                RemoveBrick(bridge);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            ClearBrick();

            animator.SetInteger("renwu", 2);

            OnWin?.Invoke(this, EventArgs.Empty);

            Invoke(nameof(WinLevel), 2f);
        }
        else if (other.CompareTag("PlayerBrick") || other.CompareTag("MoveBrick"))
        {
            //Debug.Log("AddBrick");
            AddBrick(other);
        }
    }

    private void WinLevel()
    {
        OnWinLevel?.Invoke(this, EventArgs.Empty);
    }

    private void AddBrick(Collider collider)
    {
        BrickPlayer brickPlayer = collider.GetComponentInParent<BrickPlayer>();
        if (brickPlayer != null)
        {
            if (!brickPlayer.IsTake())
            {
                /*
                if (isJustDown)
                {
                    visualGameObject.transform.position += new Vector3(0, 0.45f, 0);
                    isJustDown = false;
                }
                */
                //Debug.Log("PlayerBrick");
                //GameObject brick = Instantiate(brickPb, visualGameObject.transform.position, brickPb.transform.rotation, this.transform);
                //brickList.Add(brick);
                brickCount++;
                if (brickCount > brickList.Count)
                {
                    //GameObject brick = Instantiate(brickPb, visualGameObject.transform.position, brickPb.transform.rotation, this.transform);
                    //brickList.Add(brick);
                    if (!isFirstTake)
                    {
                        brickPlayer.SetBrickForPlayer(visualGameObject.transform.position, this.gameObject);
                        brickList.Add(brickPlayer.GetBrick());
                    }
                    else
                    {
                        brickPlayer.SetBrickForPlayer(brickPlayer.GetFirstPosOfBrick(), this.gameObject);
                        brickList.Add(brickPlayer.GetBrick());
                    }
                }
                else
                {
                    brickList[brickCount - 1].SetActive(true);
                    brickPlayer.HideBrick();
                }

                if (isFirstTake)
                {
                    isFirstTake = false;
                }
                else
                {
                    visualGameObject.transform.position += new Vector3(0, 0.45f, 0);
                }
                brickPlayer.SetTake();
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
            //Destroy(brickTemp);
            brickTemp.SetActive(false);
            //visualGameObject.transform.position += new Vector3(0, -0.45f, 0);
        }

        visualGameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void GetDestination(Direction direction)
    {
        //Debug.Log(direction);
        if (direction == Direction.None)
        {
            return;
        }

        tutorialUI.SetActive(false);

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
                        //Debug.Log("MoveBrick");
                        if (direction == moveBrick.firstDirection)
                        {
                            newDirection = moveBrick.secondDirection;
                            curMoveBrick = moveBrick;
                        }
                        else if (direction == moveBrick.secondDirection)
                        {
                            newDirection = moveBrick.firstDirection;
                            curMoveBrick = moveBrick;
                        }
                        else if (direction == moveBrick.thirdDirection)
                        {
                            newDirection = moveBrick.fourthDirection;
                            curMoveBrick = moveBrick;
                        }
                        else if (direction == moveBrick.fourthDirection)
                        {
                            newDirection = moveBrick.thirdDirection;
                            curMoveBrick = moveBrick;
                        }

                        //Debug.Log(newDirection);
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }

        //Debug.Log(destination + " " + direction);
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

        //Debug.Log(dir);
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
