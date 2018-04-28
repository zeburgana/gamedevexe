using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Children order is important fro this script to work

public class MeleeTank : MonoBehaviour
{
    
    public Transform head;
    MeleeHead headScript;

    public GameObject player;

	// Use this for initialization
	void Start () {
        //head = transform.GetChild(1);
        headScript = head.GetComponent<MeleeHead>();
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
}
