using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    
    [SerializeField]
    GameObject Player;
    [SerializeField]
    float StabilisationSpeed = 0.09f;
    [SerializeField]
    float InMovmentSpeed = 0.09f;
    [SerializeField]
    float OutMovmentSpeed = 0.15f;
    [SerializeField]
    bool DoCameraRecoil = true;
    [SerializeField]
    float MaxOffset = 10;

    //for TESTING
    [SerializeField]
    float TestingForce = 10;
    /////////

    Rigidbody2D rb2D;

    private Vector2 FollowingPos;
    private Vector2 Offset; 
    private float FollowSpeed;
    private bool isFollowingPoint = false;

    

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        FollowSpeed = StabilisationSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //fot TESTING:
        Imitate_actions();
        /////////////

        UpdateTargetPosition();
        //following 
        Vector2 cameraPos = transform.position;

        Vector2 LerpPos;
        if ((cameraPos - (Vector2) Player.transform.position).magnitude > Offset.magnitude)
           LerpPos = Vector2.Lerp(cameraPos, FollowingPos, InMovmentSpeed);
        else
            LerpPos = Vector2.Lerp(cameraPos, FollowingPos, OutMovmentSpeed);

        transform.position = new Vector3(LerpPos.x, LerpPos.y, -10);
        Debug.DrawLine(cameraPos, FollowingPos, Color.green, 2); // what camera is fllowing
        Debug.DrawLine(Player.transform.position, FollowingPos, Color.blue); // offset from player
    }

    private void Imitate_actions()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddOffset(TestingForce);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            GoToPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            FollowPlayer();
        }
    }

    // Update position of player or other object
    private void UpdateTargetPosition()
    {
        if (!isFollowingPoint)
        {
            if (DoCameraRecoil)
            {
                Vector2 PlayerPos = Player.transform.position;
                Offset = Vector2.Lerp(Offset, Vector2.zero, StabilisationSpeed);
                FollowingPos = (Vector3) (PlayerPos + Offset);
            }
            else
                FollowingPos = Player.transform.position;
        }
    }

    //Add camera recoil effect
    //magnitutde - how powerfull recoil
    //direction - to what direction (if not defined - oposite to what player is facing) 
    public void AddOffset(float magnitude, Vector2 direction = default(Vector2))
    {
        if (!isFollowingPoint && DoCameraRecoil) // only if following camera with recoil
        {
            if (direction == Vector2.zero)
            {
                float dirAngle = Player.GetComponent<PlayerMovement>().GetDirectionAngle();
                direction = new Vector2(Mathf.Cos(dirAngle), Mathf.Sin(dirAngle));
            }
            Offset = transform.position - Player.transform.position;
            Offset += (direction * magnitude * (1 -  Vector2.Dot(Offset, direction) / MaxOffset)); // further from player camera is - less powerfull recoil
            Vector2 limit = Offset.normalized * MaxOffset;
            if (Offset.magnitude > limit.magnitude)
            {
                Offset = limit;
            }
        }
    }

    public void GoToPosition(Vector3 position)
    {
        isFollowingPoint = true;
        FollowingPos = position;
    }

    public void FollowPlayer()
    {
        isFollowingPoint = false;
    }
}
