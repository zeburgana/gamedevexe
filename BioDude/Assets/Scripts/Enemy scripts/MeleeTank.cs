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
    public int localSearchLocationsTried = 0;
    private bool isTargetDestinationPlayer = false;
    public Vector2 searchAreaCenter;
    private Allerting playerAllerting;

    Animator animator;

	// Use this for initialization
	void Start () {
        //head = transform.GetChild(1);
        headScript = head.GetComponent<MeleeHead>();
        animator = GetComponent<Animator>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiPatrol = GetComponent<Patrol>();
        ai = GetComponent<IAstarAI>();
        playerAllerting = player.GetComponent<Allerting>();
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
        if (prevTargetInVision != targetInVision)
            VisionStatusChange();
        prevTargetInVision = targetInVision;

        if(isAlerted && !targetInVision) // pursuing to last known location or player if someone can see him
        {
            if (isTargetDestinationPlayer && playerAllerting.howManySeeMe == 0) // if someone saw player till now and this enemy was pursuing player
            {
                aiDestinationSetter.target = lastPositionTargetSeen;
                isTargetDestinationPlayer = false;
            }
            else if(!isTargetDestinationPlayer && playerAllerting.howManySeeMe > 0) // if someone can see player from now on this enemy should purue him
            {
                aiDestinationSetter.target = player.transform;
                isTargetDestinationPlayer = true;
            }

            if(ai.reachedEndOfPath) // went to last known location and player not seen
            {
                PerformLocalSearch();
            }
        }
    }
    

    /// <summary>
    /// reikia susikurti kintamaji kuriame bus saugoma vieta apie kuria vykdys paieska
    /// reikia update metode ideti kad jei isAllerted ir is searching ir jei kas nors mato player tai destination player..
    /// gal sukurti ant player allerting script kuriame sumuosiu kas mato kas ne ten bus galima daryti koda for allerting others
    /// </summary>
    private void PerformLocalSearch()
    {
        if(localSearchLocationsTried == 0) // just went to last player known location begining local search
        {
            searchAreaCenter = transform.position;
            aiDestinationSetter.enabled = false;
        }
        else if(localSearchLocationsTried == localSearchTryLocation) // tried all loations return to patroling
        {
            Debug.Log("finished search");
            localSearchLocationsTried = 0;
            ReturnToPatrol();
            return;
        }
        Debug.Log("location trying now " + localSearchLocationsTried);
        Debug.Log("Center: " + searchAreaCenter.ToString());
        // continue searching
        Vector2 randomLocation = new Vector2(
            Random.Range(localSearchRadius / 2 * (Random.Range(0, 2) < 1 ? 1 : -1), 
            localSearchRadius * (Random.Range(-1, 1) > 0 ? 1 : -1)), 
            Random.Range(localSearchRadius / 2 * (Random.Range(0, 2) < 1 ? 1 : -1), 
            localSearchRadius * (Random.Range(-1, 1) > 0 ? 1 : -1)));
        Debug.Log("Offset random: " + randomLocation);
        randomLocation += searchAreaCenter;
        Debug.Log("Point calc: " + randomLocation);
        localSearchLocationsTried++;
        ai.destination = randomLocation;
        ai.SearchPath();
        Debug.Log("remaining dist: " + ai.remainingDistance);
        Debug.Log("reached end" + ai.reachedEndOfPath.ToString());
        //if(ai.remainingDistance > 2 * localSearchRadius) // maybe make this into do while loop
        //search for new point beacause this map point is too far for local search
    }

    //Stop pursuing player
    public void ReturnToPatrol()
    {
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
        if (playerAllerting.howManySeeMe > 0) // if someone can see player right now
        {
            isTargetDestinationPlayer = true;
            aiDestinationSetter.target = player.transform;
        }
        else
        {
            isTargetDestinationPlayer = false;
            aiDestinationSetter.target = lastPositionTargetSeen;
        }
    }

    private void VisionStatusChange()
    {
        if (isAlerted)
        {
            if (targetInVision) // can see on its own now
            {
                playerAllerting.howManySeeMe++;
                aiDestinationSetter.target = player.transform;
                isTargetDestinationPlayer = true;
            }
            else // can't see anymore
            {
                playerAllerting.howManySeeMe--;
                if(playerAllerting.howManySeeMe == 0) // if noone can see player
                {
                    isTargetDestinationPlayer = false;
                    aiDestinationSetter.target = lastPositionTargetSeen;
                }
            }
        }
        else
        {
            if (targetInVision)
            {
                playerAllerting.howManySeeMe++;
                PursuePlayer();
            }
            else //if someone will force to not allerted then enemy can see player
                playerAllerting.howManySeeMe--;
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
