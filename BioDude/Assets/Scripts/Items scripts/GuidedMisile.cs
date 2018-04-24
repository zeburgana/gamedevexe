using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GuidedMisile : MonoBehaviour {

    public float speed = 5f;
    public float rotSpeed = 200f;

    private Rigidbody2D body;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -10; // select distance = 10 units from the camera
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - body.position;

        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        body.angularVelocity = -rotateAmount * rotSpeed;

        body.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" ||
            collision.collider.tag == "Player")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.collider.tag == "Wallmap")
        {
            Destroy(gameObject);
        }
    }
}
