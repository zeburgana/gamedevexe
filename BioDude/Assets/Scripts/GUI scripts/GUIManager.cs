using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    public Slider healthBar;
    public Text HPText;
    public player playerCharacter;
    public PlayerAmmoManager playerAmmo;
    public Text AmmoText;

    // Use this for initialization
    void Start()
    {
        healthBar.value = playerCharacter.healthCurrent;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = Character.healthMax;
        HPText.text = "HP: " + playerCharacter.healthCurrent + "/" + Character.healthMax;
        AmmoText.text = playerAmmo.currentClipAmmo + "/" + playerAmmo.currentAmmo;
    }
}
