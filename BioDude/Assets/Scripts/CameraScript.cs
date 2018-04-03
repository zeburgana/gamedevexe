using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    
    [SerializeField]
    GameObject Player;
    [SerializeField]
    float smooth = 0.09f;
    [SerializeField]
    float TestingForce;

    Rigidbody2D rb2D;
    

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 playerPos = Player.transform.position;
        Vector3 cameraPos = transform.position;
        Debug.DrawLine(playerPos, Player.GetComponent<PlayerMovement>()., Color.green, 2);
        playerPos.z = cameraPos.z;
        transform.position = Vector3.Lerp(cameraPos, playerPos, smooth);
	}

    // add force to camera (for ex.: recoil)
    public void addForce (Vector3 direction, float power)
    {
        direction.Normalize();
        direction.z = 0;
        rb2D.AddForce(direction * power);
    }


    //FOR TESTING
    void applyForce()
    {
        if (Input.GetKeyDown("F"))
        {
            Vector2 playerPos = Camera.main.WorldToViewportPoint(transform.position);
            Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            addForce(playerPos - mousePos, TestingForce);
        }
    }
}
