using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectiveObject : MonoBehaviour {

    public Vector3 currDir;
    public LayerMask collisionMask;
    public float maxStepDistance = 200;
    private LayerMask wallMask;


    public void Awake()
    {
        wallMask = LayerMask.GetMask("Collider");
    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
    }


    private float VectorToAngle(Vector2 vect)
    {
        return Mathf.Atan2(vect.y, vect.x) * Mathf.Rad2Deg - 90;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            
            Debug.DrawRay(collision.transform.position, collision.transform.up, Color.green, 1f);
            Ray ray = new Ray(collision.transform.position, collision.transform.up);
            RaycastHit2D bulletHitWall = Physics2D.Raycast(collision.transform.position, collision.transform.up, Mathf.Infinity, wallMask);
            if (bulletHitWall.collider != null)
            {
                Debug.Log(bulletHitWall.collider.name);
                Debug.Log(bulletHitWall.normal);
                var rotation = Vector2.Reflect(collision.transform.GetComponent<Rigidbody2D>().velocity, bulletHitWall.normal);
                Debug.Log(rotation);
                collision.transform.rotation = Quaternion.Euler(0f, 0f, VectorToAngle(rotation)) ;
                Debug.Log(Quaternion.Euler(0f, 0f, VectorToAngle(rotation)));
            }
            else Debug.Log("null");
            

        }
    }
}
