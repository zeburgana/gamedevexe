using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public int playerMaxHealth;
    public int playerCurrentHealth;
    public PauseMenu PausemenuCanvas;
    private bool IsPlayerAlive;

    // Use this for initialization
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        IsPlayerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth <= 0 && IsPlayerAlive == true)
        {
            IsPlayerAlive = false;
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
        
        //PausemenuCanvas.killtest();
        StartCoroutine(PausemenuCanvas.PlayerDeath());
    }

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void setMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }


    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded = true;
    }
}