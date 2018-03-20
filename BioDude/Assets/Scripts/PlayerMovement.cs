using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    float speed;
    private Rigidbody2D rb2D;
    [SerializeField]
    GameObject pistolBullet;
    [SerializeField]
    AudioSource pistolFire;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        Looking();
	}

    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, moveY * speed);
        //rb2D.AddForce(move * speed);
    }

    private void Looking()
    {
        Vector2 playerPos = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenToPoints(playerPos, mousePos);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle+90));
        if (Input.GetMouseButtonDown(0))
            Shooting();
    }

    private void Shooting()
    {
        //sometimes bullet spawns behind the player :D
        pistolFire.Play();
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        Instantiate(pistolBullet, new Vector3(x, y, z), transform.rotation);
    }

    private float AngleBetweenToPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
