using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    GameObject pistolBullet;
    [SerializeField]
    AudioSource pistolFire;

//unused     private Rigidbody2D rb2D;
    private float directionAngle;

    // Use this for initialization
    void Start () {
//unused         rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
        directionAngle = AngleBetweenToPoints(playerPos, mousePos);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, directionAngle * Mathf.Rad2Deg + 90));
        if (Input.GetMouseButtonDown(0))
            Shooting();
    }

    public float GetDirectionAngle()
    {
        //return AngleBetweenToPoints(transform.position, Input.mousePosition) + 90;
        return directionAngle;
    }

    private void Shooting()
    {
        //sometimes bullet spawns behind the player :D
        if (GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded && GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon != null)
        {
            pistolFire.Play();
            float x = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").transform.position.x;
            float y = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").transform.position.y;
            float z = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").transform.position.z;
            GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded = false;
            StartCoroutine("Cooldown");
            Instantiate(pistolBullet, new Vector3(x, y, z), transform.rotation);
        }
    }

    private float AngleBetweenToPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon.GetComponent<Weapon>().cooldown);
        GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded = true;
    }
}
