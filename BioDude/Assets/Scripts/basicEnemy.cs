using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

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
    [SerializeField]
    private int moveY;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        RGB = GetComponent<Rigidbody2D>();
        CircleCol = GetComponent<CircleCollider2D>();
    }
    // Use this for initialization
    private void Start () {
        //Equip(0, 0, 0, 0, 0, 0, 0);
        IsMoving = false;
	}
	
	// Update is called once per frame
	void Update () {

        /*RaycastHit2D collisionX = Physics2D.Raycast(transform.position, new Vector2(moveX, 0), 0.15f);
        RaycastHit2D collisionY = Physics2D.Raycast(transform.position, new Vector2(0, moveY), 0.16f);
        
        if(collisionX.collider.tag == "Wall")
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
        if (collisionY.collider.tag == "Wall")
        {
            if (moveY == 1)
            {
                moveY = -1;
            }
            else
            {
                moveY = 1;
            }
        }*/
        Move(moveX, moveY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
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
        /*if (collision.collider.tag == "Wall")
        {
            if (moveY == 1)
            {
                moveY = -1;
            }
            else
            {
                moveY = 1;
            }
        }*/
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
    }
    private void walkDown()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        SendAnimInfo(-1, 0);
    }
    private void walkLeft()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        SendAnimInfo(0, -1);
    }
    private void walkRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        SendAnimInfo(0, 1);
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
