using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 3;
    Weapon weapon;
    
    void Start ()
    {
        weapon = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon.GetComponent<Weapon>();
        Destroy(gameObject, weapon.timeUntilSelfDestrucion);
        //maybe just need to use velocity, not addRelativeForce
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, weapon.projectileSpeed)); //transform.forward.z * speed - 10, transform.forward.z * speed + 60

    }

    void Update ()
	{}

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Character>().Damage(damage);
            Destroy(gameObject);
        }
        else
        	Destroy(gameObject);
    }
}
