using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Children order is important fro this script to work

public class MeleeTank : MonoBehaviour
{
    //this should inherit base class with damage and other variables and maybe script for damaging player
    public int damage = 1;
    
    public Transform head;
    MeleeHead headScript;
    public GameObject player;

    Animator animator;

    private bool TargetInRange = false;

	// Use this for initialization
	void Start () {
        //head = transform.GetChild(1);
        headScript = head.GetComponent<MeleeHead>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        SetHeadDirection();
    }

    //reiktu padaryti kad ne kiekviena frame o reciau skaiciuotu
    private void SetHeadDirection()
    {
        Vector2 direction = player.transform.position - transform.position;
        headScript.targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TargetInRange = true;
            animator.SetBool("TargetInRange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TargetInRange = false;
            animator.SetBool("TargetInRange", false);
        }
    }

    private void PerformAttack() // a smarter way of inflicting damage is required - this is nether secure nor efficient 
    {
        if (TargetInRange)
        {
            player.GetComponent<PlayerHealthManager>().HurtPlayer(damage);
        }
    }
}
