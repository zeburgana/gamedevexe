using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    
    [SerializeField]
    GameObject Player;
    [SerializeField]
    float FollowingSpeed = 0.09f;
    [SerializeField]
    float TestingForce = 10;
    [SerializeField]
    byte CameraMode = 0; // 0 - following player; 1 - following Pl with recoil; 2 - going to given position 
    [SerializeField]
    float MaxOffset = 10;

    Rigidbody2D rb2D;

    private Vector2 FollowingPos;
    private Vector2 Offset; 
    private float FollowSpeed;

    

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        FollowSpeed = FollowingSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        UpdateTargetPosition();
        //following 
        Vector2 cameraPos = transform.position;
        Debug.DrawLine(cameraPos, FollowingPos, Color.green, 2);
        Vector2 LerpPos = Vector2.Lerp(cameraPos, FollowingPos, FollowSpeed);
        transform.position = new Vector3(LerpPos.x, LerpPos.y, -10);
	}


    // Update position of player or other object
    private void UpdateTargetPosition()
    {
        switch (CameraMode)
        {
            case 0: // following player
                FollowingPos = Player.transform.position;
                break;
            case 1: // following Pl with recoil
                FollowingPos = Player.transform.position + (Vector3) Offset;
                break;
            case 2: // following given position
                break;
        }
        //if following player
    }

    //Add camera recoil effect
    //magnitutde - how powerfull recoil
    //direction - to what direction (if not defined - oposite to what player is facing) 
    public void AddOffset(float magnitude, Vector2 direction = default(Vector2))
    {
        if (CameraMode == 1) // only if following camera with recoil
        {
            if (direction == Vector2.zero)
            {
                float dirAngle = Player.GetComponent<PlayerMovement>().GetDirectionAngle();
                direction = new Vector2(-Mathf.Sin(dirAngle), -Mathf.Cos(dirAngle));
            }
            Offset = transform.position - Player.transform.position;
            Offset += direction * magnitude * Offset.magnitude / MaxOffset; // further from player camera is - less powerfull recoil
            Offset = Vector2.Min(Offset.normalized * MaxOffset, Offset);
        }
    }

    public void GoToPosition(Vector3 position)
    {
        CameraMode = 2;
        FollowingPos = position;
    }

    public void FollowPlayer()
    {
        CameraMode = 1;
    }
}
