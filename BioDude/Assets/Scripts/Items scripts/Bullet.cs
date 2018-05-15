using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage = 0;
    //bool bulletFired = false;  //////////////////////////////

    /// <summary>
    /// give bullet speed and time after which it should destroy itself
    /// </summary>
    /// <param name="destroyAfter">time in second after how long destroy this bullet</param>
    /// <param name="speed">speed to give to bullet</param>
    public void Instantiate(float destroyAfter, float speed, float damage)
    {
        Destroy(gameObject, destroyAfter);
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, speed));
        this.damage = damage;
    }

	private void OnCollisionEnter2D(Collision2D collision)
    {
        Character charHealth = collision.gameObject.GetComponent<Character>();
        if (charHealth != null)
        {
            charHealth.Damage(damage);
        }
        Destroy(gameObject);
    }
/*
    IEnumerator Fire()
    {
        yield return new WaitForFixedUpdate();

        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, weapon.projectileSpeed));
    }*/
}
