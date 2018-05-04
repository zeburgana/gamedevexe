using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GuidedMisile : Explosive {

    public float speed = 5f;
    public float rotSpeed = 200f;

    public GameObject explosionEffect;

    public float radius = 3f;
    public float force = 500f;

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
        Explode();
    }
    public override void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D obj in nearbyObjects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log("rigidbody found");
                AddExplosionForce(rb, force, transform.position, radius);
            }
        }
        Destroy(gameObject);
    }

}
