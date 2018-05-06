using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : Character
{
    public float speed { get; set; }            // The speed that the player will move at.

    Vector2 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody2D playerRigidbody;          // Reference to the player's rigidbody.

    public PauseMenu PausemenuCanvas;
    private float directionAngle;
    private WeaponManager weapon; 
    public List<Explosive> GrenadeList; // BULLSHIT  reikia karkur kitur deti
    Explosive selectedGrenade;
    public float throwForce = 5000f;

    protected override void Start()
    {
        base.Start();
        Instantiate();
    }

    void Awake()  //BULLSHIT kuo skiriasi nuo start?
    {
        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        if (GrenadeList.Count > 0)
            selectedGrenade = GrenadeList[0];
        weapon = GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>(); // BULLHIT  negalima naudot tag tokiam dalykui turbut, o kas kai bus daugiauweponslotu playerio?
        speed = 210;
    }

    private void Instantiate()
    {

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Controls();
        Turning(); // Turn the player to face the mouse cursor.
        Animating(h, v); // Animate the player.
    }

    void Controls() //BULLSHIT reikia susitvarkyti ir apgalvoti ar viskas bus ok jei vienu framu pasileistu visos komandos nes nenaudojami else if - galbut reikia debouncing arba kintamuju delayinti veiksma kitam framui  
    {
        if (Input.GetButtonDown("Fire"))
            weapon.Shoot();
        if (Input.GetButtonDown("ThrowGranade"))
            UseGrenade();
        if (Input.GetButtonDown("Reload"))
            Reload();
    }

    void Move(float h, float v)
    {
        //playerRigidbody.velocity = new Vector2(h * speed, v * speed);  //option1
        Debug.Log("moving");
        Vector3 movement = new Vector3(h, v, 0);         //option2
        playerRigidbody.AddForce(movement * speed);
    }

    void Turning()
    {
        //Vector2 playerPos = Camera.main.WorldToViewportPoint(transform.position);   //old rotation method
        //Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //directionAngle = Mathf.Atan2(mousePos.y - playerPos.y, mousePos.x - playerPos.x);
        //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, directionAngle * Mathf.Rad2Deg - 90));
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        Vector2 playerPos = Camera.main.ScreenToWorldPoint(transform.position);

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        Debug.DrawLine(transform.position, transform.position + 10 * transform.up);
    }

    void Animating(float h, float v)  // THIS WILL BE USED kai turesim playeri !!!!!!
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }

    // OVERRIDEN METHODS:

    protected override void Die()
    {
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        //^^^ pakeist i player death animation
        Debug.Log("death");

        //PausemenuCanvas.killtest();
        StartCoroutine(PausemenuCanvas.PlayerDeath());
    }

    // OTHER METHODS:

    public float GetDirectionAngle()
    {
        return directionAngle;
    }

    private void Reload()
    {
        if ( weapon.currentAmmo > 0 && !weapon.isReloading && weapon.currentClipAmmo != weapon.clipSize)
            StartCoroutine(weapon.Reload());

    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        GameObject.FindGameObjectWithTag("PlayerWeaponSlot").GetComponent<WeaponManager>().cooldownEnded = true;
    }

    private void UseGrenade()
    {
        Vector3 instantiatePos = transform.position;
        Debug.Log("throw");
        if (selectedGrenade != null)
        {
            Explosive nade = Instantiate(selectedGrenade, instantiatePos, transform.rotation);
            nade.Throw(throwForce);
        }
    }
}