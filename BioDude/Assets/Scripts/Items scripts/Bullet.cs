using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 3;
    Weapon weapon;
    bool bulletFired = false;
    
    void Start ()
    {
        weapon = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().activeWeapon.GetComponent<Weapon>();

        Destroy(gameObject, weapon.timeUntilSelfDestrucion);
        var randomNumberZ = Random.Range(-weapon.accuracy*0.1f, weapon.accuracy*0.1f);
        Vector3 dir = transform.rotation.eulerAngles;
        dir.z += randomNumberZ;
        transform.eulerAngles = dir;
        StartCoroutine(Fire());
    }

    /*void FixedUpdate ()
	{
        if (!bulletFired)
        {
            bulletFired = true;
        }
    }*/

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
    IEnumerator Fire()
    {
        yield return new WaitForFixedUpdate();

        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, weapon.projectileSpeed));
    }
}
