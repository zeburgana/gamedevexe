using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D RGB;
    private CircleCollider2D CircleCol;

    [SerializeField]
    private int HeadGear;
    [SerializeField]
    private int BodyGear;
    [SerializeField]
    private int LegGear;
    [SerializeField]
    private int FootGear;
    [SerializeField]
    private int BackGear;
    [SerializeField]
    private int LeftHand;
    [SerializeField]
    private int RightHand;

    [SerializeField]
    private float speed;
    private bool IsMoving;
    [SerializeField]
    private int moveX;
    private int lastMoveX;
    [SerializeField]
    private int moveY;
    private int lastMoveY;

    private int direction = 0;

    [SerializeField]
    private bool alerted;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        RGB = GetComponent<Rigidbody2D>();
        CircleCol = GetComponent<CircleCollider2D>();
    }
    // Use this for initialization
    private void Start()
    {
        //Equip(0, 0, 0, 0, 0, 0, 0);
        IsMoving = false;
    }

    // Update is called once per frame
    void Update()
    {

        /*RaycastHit2D collisionX = Physics2D.Raycast(transform.position, new Vector2(moveX, 0), 0.15f);
        RaycastHit2D collisionY = Physics2D.Raycast(transform.position, new Vector2(0, moveY), 0.16f);
        */
        Move(moveX, moveY);
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            alerted = true;
            lastMoveX = moveX;
            lastMoveY = moveY;
        }
            
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            alerted = false;
            //moveX = lastMoveX;
            //moveY = lastMoveY;
        }
            
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (alerted && collision.gameObject.tag == "Player")
        {
            followPlayer(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
        }
    }
    private void followPlayer(float x, float y)
    {
        if (Mathf.Abs(transform.position.x - x) > Mathf.Abs(transform.position.y - y))
        {
            if (transform.position.x > x)
            {
                moveX = -1;
                moveY = 0;
            }
            else
            {
                moveX = 1;
                moveY = 0;
            }
        }
        else
        {
            if (transform.position.y > y)
            {
                moveY = -1;
                moveX = 0;
            }
            else
            {
                moveY = 1;
                moveX = 0;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!alerted)
        {
            if (direction == 3 || direction == 4)
                if (collision.collider.tag == "Wallmap")
                {
                    if (moveX == 1)
                    {
                        moveX = -1;
                    }
                    else
                    {
                        moveX = 1;
                    }
                }
            if (direction == 1 || direction == 2)
                if (collision.collider.tag == "Wallmap")
                {
                    if (moveY == 1)
                    {
                        moveY = -1;
                    }
                    else
                    {
                        moveY = 1;
                    }
                }
        }
    }


    //Put item id's inside, if id == 0, there is no gear
    public void Equip(int head, int body, int leg, int foot, int back, int left, int right)
    {
        HeadGear = head;
        BodyGear = body;
        LegGear = leg;
        FootGear = foot;
        BackGear = back;
        LeftHand = left;
        RightHand = right;
    }

    void SendAnimInfo(int vert, int horiz)
    {
        animator.SetFloat("XSpeed", horiz);
        animator.SetFloat("YSpeed", vert);
        animator.SetBool("IsMoving", IsMoving);
    }



    private void walkUp()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        SendAnimInfo(1, 0);
        direction = 1;
    }
    private void walkDown()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        SendAnimInfo(-1, 0);
        direction = 2;
    }
    private void walkLeft()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        SendAnimInfo(0, -1);
        direction = 3;
    }
    private void walkRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        SendAnimInfo(0, 1);
        direction = 4;
    }



    private void Move(int directionX, int directionY)
    {
        switch (directionX)
        {
            case -1:
                walkLeft();
                break;
            case 1:
                walkRight();
                break;
            case 0:
                break;
        }
        switch (directionY)
        {

            case 1:
                walkUp();
                break;
            case 0:
                break;
            case -1:
                walkDown();
                break;
        }
        IsMoving = true;

    }
 
}