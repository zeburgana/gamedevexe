using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public int playerMaxHealth;
    public int playerCurrentHealth;
    public GameObject PauseMenu;

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
            gameObject.SetActive(false);
            playerCurrentHealth = 0;
        }
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