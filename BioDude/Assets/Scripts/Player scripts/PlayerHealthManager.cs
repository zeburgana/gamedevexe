using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public int playerMaxHealth;
    public int playerCurrentHealth;
    public GameObject PausemenuUI;
    public GameObject DeathSplashImage;
    public GameObject GameoverMenu;

    // Use this for initialization
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth <= 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        //^^^ pakeist i player death animation
        Debug.Log("death");
        playerCurrentHealth = 0;
        PausemenuUI.SetActive(true);
        //Pausemenu.GetComponent<Animator>().Play("DeathImageSplash");
        DeathSplashImage.SetActive(true);
        DeathSplashImage.GetComponent<Animator>().Play("DeathImageSplash");
        StartCoroutine(DeathWait());
    }

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void setMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(3);
        GameoverMenu.SetActive(true);
        gameObject.SetActive(false);

    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded = true;
    }
}