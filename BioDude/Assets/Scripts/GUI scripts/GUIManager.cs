using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    public Slider healthBar;
    public Text HPText;
    public PlayerHealthManager playerHealth;
    public PlayerAmmoManager playerAmmo;
    public Text AmmoText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = playerHealth.playerMaxHealth;
        healthBar.value = playerHealth.playerCurrentHealth;
        if (playerHealth.playerCurrentHealth < 0)
            playerHealth.playerCurrentHealth = 0;
        HPText.text = "HP: " + playerHealth.playerCurrentHealth + "/" + playerHealth.playerMaxHealth;
        AmmoText.text = playerAmmo.currentClipAmmo + "/" + playerAmmo.currentAmmo;
    }
}
