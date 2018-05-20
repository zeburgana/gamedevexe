using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : Character
{
    public float speed { get; set; }            // The speed that the player will move at.

    Vector2 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody2D playerRigidbody;          // Reference to the player's rigidbody.
    bool PlayerAlive = true;

    public PauseMenu PausemenuCanvas;
    private float rot_z;
    private WeaponManager weaponManager; 

    void Awake()
    {
        Initiate();
        // Set up references.
        anim = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        weaponManager = transform.GetComponent<WeaponManager>();
        speed = 210;
    }

    override protected void Initiate()
    {
        healthMax = 100;

        if (PlayerPrefs.HasKey("PlayerHP"))
        {
            //Debug.Log("FOUND PLAYER HP");
            healthCurrent = PlayerPrefs.GetFloat("PlayerHP");
        }
        else
        {
            //Debug.Log("PLAYER HP NOT FOUND");
            base.Initiate();
        }
    }

    private void Update()
    {
        if(PlayerAlive)
        {
            Controls();
            Turning(); // Turn the player to face the mouse cursor.
        }
    }

    void FixedUpdate()
    {
        if (PlayerAlive)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Move(h, v);
            Animating(h, v); // Animate the player. //BULLSHIT kam nurodyti h ir v jei jau bus issaugota i movement vectoriu tik atsargiai kad nepakelti auksciau nes tada nebus
        }
    }

    void Controls() //BULLSHIT reikia susitvarkyti ir apgalvoti ar viskas bus ok jei vienu framu pasileistu visos komandos nes nenaudojami else if - galbut reikia debouncing arba kintamuju delayinti veiksma kitam framui  
    {
        if (Input.GetButtonDown("Fire"))
            weaponManager.Shoot();
        if (Input.GetButtonDown("ThrowGranade"))
            weaponManager.UseExplosive();
        if (Input.GetButtonDown("Reload"))
            Reload();
        if (Input.GetButtonDown("SwitchWeaponRight"))
            weaponManager.SwitchWeaponRight();
        if (Input.GetButtonDown("SwitchWeaponLeft"))
            weaponManager.SwitchWeaponLeft();
        if (Input.GetButtonDown("SwitchGrenadeRight"))
            weaponManager.SwitchExplosiveRight();
        if (Input.GetButtonDown("SwitchGrenadeLeft"))
            weaponManager.SwitchExplosiveLeft();
        if (Input.GetButtonDown("SelectKnife"))
            weaponManager.SelectWeaponByIndex(-1);
    }

    void Move(float h, float v)
    {
        //playerRigidbody.velocity = new Vector2(h * speed, v * speed);  //option1
        movement = new Vector2(h, v);         //option2
        playerRigidbody.AddForce(movement * speed);
    }

    void Turning()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        Vector2 playerPos = Camera.main.ScreenToWorldPoint(transform.position);

        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsMoving", walking);
        //Vector2 rot = transform.rotation;
        Vector3 relative = transform.InverseTransformVector(movement);
        anim.SetFloat("XSpeed", relative.x);
        anim.SetFloat("YSpeed", relative.y);
    }

    // OVERRIDEN METHODS:

    protected override void Die()
    {
        PlayerAlive = false;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        //^^^ pakeist i player death animation

        StartCoroutine(PausemenuCanvas.PlayerDeath());
    }

    // OTHER METHODS:

    public float GetDirectionAngle()
    {
        return rot_z;
    }

    private void Reload()
    {
        weaponManager.Reload();
    }

    //IEnumerator Cooldown()
    //{
    //    yield return new WaitForSeconds(2);
    //    weaponManager.cooldownEnded = true;
    //}


    public void SavePlayerStats()
    {
        PlayerPrefs.SetFloat("PlayerHP", healthCurrent);
        Debug.Log("saved player hp");
        if (weaponManager != null && weaponManager.weaponArray != null)
        {
            Debug.Log("saving ammo info");
            //Saving info
            string name;
            int ammoCount;
            for (int i = 0; i < weaponManager.fireArmAmmo.Length; i++)
            {
                name = weaponManager.fireArmAmmo[i].name;
                ammoCount = weaponManager.fireArmAmmo[i].amount;
                if (name != "" && name != null && ammoCount >= 0)
                {
                    Debug.Log("saved " + ammoCount + name);

                    PlayerPrefs.SetInt(name + "Ammo", ammoCount);
                }
            }
            for (int i = 0; i < weaponManager.explosiveAmmo.Length; i++)
            {
                name = weaponManager.explosiveAmmo[i].name;
                ammoCount = weaponManager.explosiveAmmo[i].amount;
                if (name != "" && name != null && ammoCount >= 0)
                {
                    Debug.Log("saved " + ammoCount + name);
                    PlayerPrefs.SetInt(name + "Ammo", ammoCount);
                }
            }
        }
        else
            Debug.Log("failed to save ammo info");

        PlayerPrefs.Save();
    }
}