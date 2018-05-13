using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    public Slider healthBar;
    public Text HPText;
    public player playerCharacter;
    public Text AmmoText;
    public WeaponManager weaponManager;

    // Use this for initialization
    void Start()
    {
        healthBar.maxValue = playerCharacter.healthMax;

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerCharacter.GetHealth();
        HPText.text = "HP: " + playerCharacter.GetHealth() + "/" + playerCharacter.healthMax;
    }

    public void SetBulletGUI(int currentClipAmmo, int currentAmmo)
    {
        AmmoText.text = currentClipAmmo + "/" + currentAmmo;
    }
}
