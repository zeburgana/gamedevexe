using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectiveObject : MonoBehaviour {


    public LayerMask collisionMask;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            Debug.Log(collision.transform);
            Ray2D ray = new Ray2D(collision.transform.position, collision.transform.forward);
            RaycastHit2D hit;
            if (Physics2D.Raycast(ray, hit,5000.0f, collisionMask))
            {

            }
            Vector2 reflectDir = Vector2.Reflect(ray.direction, hit.normal);
        }
    }
}
