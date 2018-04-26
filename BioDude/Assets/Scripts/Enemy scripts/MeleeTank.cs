using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Children order is important fro this script to work

public class MeleeTank : Pathfinding2D
{

    Transform legs;
    Transform head;
    Pathfinding2D pathfind;

    public GameObject player;

	// Use this for initialization
	void Start () {
        legs = transform.GetChild(0);
        head = transform.GetChild(1);
        
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Find path");
            FindPath(transform.position, player.transform.position);
        }
        if (Path.Count > 0)
        {
            Move();
            //transform.position = Vector3.MoveTowards(transform.position, pathfind.Path[0], Time.deltaTime * 30F);
            //if (Vector3.Distance(transform.position, pathfind.Path[0]) < 0.1F)
            //{ pathfind.Path.RemoveAt(0); }
        }
    }
}
