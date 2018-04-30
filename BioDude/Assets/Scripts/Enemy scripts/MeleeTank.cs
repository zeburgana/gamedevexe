using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// NOTE: Children order is important fro this script to work

public class MeleeTank : MonoBehaviour
{
    //this should inherit base class with damage and other variables and maybe script for damaging player
    public int damage = 1;
    
    public Transform head;
    MeleeHead headScript;
    public GameObject player;
    public bool isAlerted = false;
    public float visionAngle = 30; // vision angle
    public float visionRange = 10;
    public LayerMask obstacleMask;
    public Transform lastPositionTargetSeen;
    public float localSearchRadius = 5; // distance - how far to search for player from last known position
    public int localSearchTryLocation = 3; // how many locations to try after target lost
    IAstarAI ai;

    //private:
    public bool targetInVision = false;
    public float distanceToPlayer;
    private bool targetInAttackRange = false;
    private AIDestinationSetter aiDestinationSetter;
    private Patrol aiPatrol;
    private bool prevTargetInVision = false;

    Animator animator;

	// Use this for initialization
	void Start () {
        //head = transform.GetChild(1);
        headScript = head.GetComponent<MeleeHead>();
        animator = GetComponent<Animator>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiPatrol = GetComponent<Patrol>();
        ai = GetComponent<IAstarAI>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        //Set head target direction:
        if (targetInVision)
            headScript.targetAngle = VectorToAngle(direction);
        else
            headScript.targetAngle = VectorToAngle(transform.up); // this should always set head rotation to zero. better solution required

        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        targetInVision = false;
        if (distanceToPlayer < visionRange) // if in range
        {
            if (Vector3.Angle(head.transform.up, direction) < visionAngle) // if in vision angle
            {
                if(!Physics2D.Raycast(transform.position, direction, distanceToPlayer, obstacleMask)) //if no obstacles in between
                {
                    targetInVision = true;
                    lastPositionTargetSeen.position = player.transform.position; // Needs to be updated because other tanks might not see player and would attempt to go to last known location
                }
            }
        }
        if (prevTargetInVision != targetInAttackRange)
            VisionStatusChange();
        prevTargetInVision = targetInVision;
        if(isAlerted && !targetInVision) // pursuing to last known location
        {
            if(ai.reachedEndOfPath) // went to last known location and player not seen
            {
                PerformLocalSearch();
                ReturnToPatrol();
            }
        }
    }
    

    /// <summary>
    /// reikia susikurti kintamaji kuriame bus saugoma vieta apie kuria vykdys paieska
    /// reikia update metode ideti kad jei isAllerted ir is searching ir jei kas nors mato player tai destination player..
    /// 
    /// </summary>
    private void PerformLocalSearch()
    {
        Vector2 RandomLocation = new Vector2();
    }

    //Stop pursuing player
    public void ReturnToPatrol()
    {
        PerformLocalSearch();
        aiDestinationSetter.enabled = false;
        aiPatrol.enabled = true;
        isAlerted = false;
    }

    //call this method to make enemy go to last known player position
    public void PursuePlayer()
    {
        isAlerted = true;
        aiPatrol.enabled = false;
        aiDestinationSetter.enabled = true;
        aiDestinationSetter.target = lastPositionTargetSeen;
    }

    private void VisionStatusChange()
    {
        if (isAlerted)
        {
            if (targetInVision) // can see on its own now
            {
                aiDestinationSetter.target = player.transform;
            }
            else // can't see anymore
            {
                aiDestinationSetter.target = lastPositionTargetSeen;
                //check if last known pos reached? then go to patrol

            }
        }
        else
        {
            if (targetInVision)
            {
                PursuePlayer();
                aiDestinationSetter.target = player.transform;
            }
        }
        
    }

    // these might be placed in more general location
    private float VectorToAngle(Vector2 vect)
    {
        return Mathf.Atan2(vect.y, vect.x) * Mathf.Rad2Deg - 90;
    }
    private Vector2 AngleToVector(float angle)
    {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            targetInAttackRange = true;
            animator.SetBool("TargetInRange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            targetInAttackRange = false;
            animator.SetBool("TargetInRange", false);
        }
    }

    private void PerformAttack() // a smarter way of inflicting damage is required - this is nether secure nor efficient 
    {
        if (targetInAttackRange)
        {
            player.GetComponent<PlayerHealthManager>().HurtPlayer(damage);
        }
    }
}
