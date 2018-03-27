using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : MonoBehaviour {


	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2f);
    }
	
	// Update is called once per frame
	void Update () {
        //maybe just need to use velocity, not addRelaticeForce
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon.GetComponent<Weapon>().projectileSpeed)); //transform.forward.z * speed - 10, transform.forward.z * speed + 60
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            SelfDestruct();
            DestroyObject(collision);
        }
        if (collision.collider.tag == "Wallmap")
        {
            SelfDestruct();
        }
    }
    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
    private void DestroyObject(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
