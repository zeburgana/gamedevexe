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

    private Rigidbody2D rb2D;
    private float directionAngle;

    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
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
        if (Input.GetKeyDown(KeyCode.R) && GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentAmmo > 0 && GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().isReloading == false && GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentClipAmmo != GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().clipSize)
            StartCoroutine(GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().Reload());
    }

    public float GetDirectionAngle()
    {
        //return AngleBetweenToPoints(transform.position, Input.mousePosition) + 90;
        return directionAngle;
    }

    private void Shooting()
    {
        //sometimes bullet spawns behind the player :D
        if (GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded && GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon != null && GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().isReloading == false)
        {
            if (GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentClipAmmo > 0)
            {
                pistolFire.Play();
                float x = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").transform.position.x;
                float y = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").transform.position.y;
                float z = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").transform.position.z;
                GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded = false;
                GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentClipAmmo--;
                StartCoroutine("Cooldown");
                Instantiate(pistolBullet, new Vector3(x, y, z), transform.rotation);
                if (GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentClipAmmo == 0 && GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().currentAmmo > 0)
                    StartCoroutine(GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().Reload());
            }
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
