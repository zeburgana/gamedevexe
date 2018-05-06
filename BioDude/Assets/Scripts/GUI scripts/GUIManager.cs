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
        healthBar.maxValue = playerCharacter.healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerCharacter.healthCurrent;
        HPText.text = "HP: " + playerCharacter.healthCurrent + "/" + playerCharacter.healthMax;
        AmmoText.text = playerAmmo.currentClipAmmo + "/" + playerAmmo.currentAmmo;
    }
}
