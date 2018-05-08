using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// NOTE: Children order is important fro this script to work

public class MeleeTank : Character
{
    public int damage = 1;

    public Transform head;
    public GameObject player;
    public bool isAlerted = false;
    public float visionAngle = 30; // vision angle (half of it)
    public float visionRange = 10;
    public LayerMask obstacleMask;
    public Transform lastPositionTargetSeen;
    public float localSearchRadius = 5; // distance - how far to search for player from last known position
    public int localSearchTryLocation = 3; // how many locations to try after target lost
    public int fromAngleToTurnHead = 15;
    public int toAngleToTurnHead = 90; // max value should be 90 for realistic reaction
    public int localSearchLookAroundTimes = 2;

    IAstarAI ai;

    //private:
    private bool targetInVision = false;
    private float distanceToPlayer;
    private bool targetInAttackRange = false;
    private AIDestinationSetter aiDestinationSetter;
    private Patrol aiPatrol;
    private bool prevTargetInVision = false;
    private int localSearchLocationsTried = 0;
    private int localSearchLookedAround = 0;
    private bool isTargetDestinationPlayer = false;
    private Vector2 searchAreaCenter;
    private Allerting playerAllerting;
    private Head headScript;

    Animator animator;

    // Use this for initialization
    void Start()
    {
        //head = transform.GetChild(1);
        headScript = head.GetComponent<Head>();
        animator = GetComponent<Animator>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiPatrol = GetComponent<Patrol>();
        ai = GetComponent<IAstarAI>();
        playerAllerting = player.GetComponent<Allerting>();
    }

    // Update is called once per frame
    void Update()
    {
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
                if (!Physics2D.Raycast(transform.position, direction, distanceToPlayer, obstacleMask)) //if no obstacles in between
                {
                    targetInVision = true;
                    //lastPositionTargetSeen.position = player.transform.position; // Needs to be updated because other tanks might not see player and would attempt to go to last known location
                }
            }
        }
        if (prevTargetInVision != targetInVision)
            VisionStatusChange();
        prevTargetInVision = targetInVision;

        if (isAlerted && !targetInVision) // pursuing to last known location(local search) or player if someone can see him
        {
            if (isTargetDestinationPlayer && playerAllerting.howManySeeMe == 0) // if someone saw player till now and this enemy was pursuing player
            {
                PursuePlayer();
            }
            else if (!isTargetDestinationPlayer && playerAllerting.howManySeeMe > 0) // if someone can see player from now on this enemy should purue him
            {
                PursuePlayer();
            }

            if (ai.reachedEndOfPath) // went to last known location and player not seen
            {
                LookAround();
            }
        }
    }

    private void LookAround() // look arround then reached destination and after finished looking find next destination
    {
        if (headScript.isRotated) // if finished rotating to the angle
        {
            if (localSearchLookedAround == localSearchLookAroundTimes) // finished looking
            {
                localSearchLookedAround = 0;
                PerformLocalSearch();
                return;
            }
            float randomRotation = Random.Range(fromAngleToTurnHead, toAngleToTurnHead);
            randomRotation += (localSearchLookedAround % 2 == 0 ? 1 : -1) * VectorToAngle(transform.up);
            headScript.targetAngle = randomRotation;
            localSearchLookedAround++;
        }
    }

    private void PerformLocalSearch()
    {
        if (localSearchLocationsTried == 0) // just went to last player known location begining local search
        {
            searchAreaCenter = transform.position;
            aiDestinationSetter.enabled = false;
        }
        else if (localSearchLocationsTried == localSearchTryLocation) // tried all loations return to patroling
        {
            //Debug.Log("finished search");
            localSearchLocationsTried = 0;
            ReturnToPatrol();
            return;
        }
        //Debug.Log("location trying now " + localSearchLocationsTried);
        //Debug.Log("Center: " + searchAreaCenter.ToString());
        // continue searching
        Vector2 randomLocation = new Vector2(
            Random.Range(localSearchRadius / 2, localSearchRadius) * (Random.Range(0, 2) < 1 ? 1 : -1),
            Random.Range(localSearchRadius / 2, localSearchRadius) * (Random.Range(0, 2) < 1 ? 1 : -1));
        //Debug.Log("Offset random: " + randomLocation);
        randomLocation += searchAreaCenter;
        //Debug.Log("Point calc: " + randomLocation);
        localSearchLocationsTried++;
        ai.destination = randomLocation;
        ai.SearchPath();
        // Debug.Log("remaining dist: " + ai.remainingDistance);
        //Debug.Log("reached end" + ai.reachedEndOfPath.ToString());
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
        localSearchLocationsTried = 0;
        localSearchLookedAround = 0;
        aiPatrol.enabled = false;
        aiDestinationSetter.enabled = true;
        if (playerAllerting.howManySeeMe > 0) // if someone can see player right now
        {
            isTargetDestinationPlayer = true;
            aiDestinationSetter.target = player.transform;
            ai.canSearch = true;
        }
        else
        {
            isTargetDestinationPlayer = false;
            aiDestinationSetter.target = lastPositionTargetSeen;
            ai.canSearch = false;
            ai.SearchPath();
        }
    }

    private void VisionStatusChange()
    {
        if (isAlerted)
        {
            if (targetInVision) // can see on its own now
            {
                playerAllerting.howManySeeMe++;
                PursuePlayer();
            }
            else // can't see anymore
            {
                lastPositionTargetSeen.position = player.transform.position;
                playerAllerting.howManySeeMe--;
                PursuePlayer();
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
            player.GetComponent<player>().Damage(damage);
        }
    }

    protected override void Die()
    {
        // enemy death: smokes and stoped movement
        Destroy(this);
    }
}
