using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour {

    [SerializeField]
    float rotationSpeed = 10;

    Transform tank;
    Grid test;

	// Use this for initialization
	void Start () {
        tank = GetComponentInParent<Transform>();
        //test = ;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // position - set position your tank should go to.
    public void GoTo(Vector2 positiion)
    {
        
    }


}
