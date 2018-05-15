using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    /// <summary>
    /// this script should be replaced on player itself!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// </summary>

    public struct Ammo
    {
        public string name;
        public int amount; // how many shots player has
        public int maxAmount;

        /// <summary>
        /// adds ammo to inventory
        /// </summary>
        /// <param name="amount">amount of ammo to add</param>
        /// <returns>returns amount of ammo added</returns>
        public int AddAmmo(int amount)
        {
            if (amount > 0)
            {
                int desiredAmount = amount + this.amount;
                if(desiredAmount > maxAmount)
                {
                    int added = amount - desiredAmount + maxAmount;
                    this.amount = maxAmount;
                    return added;
                }
                else
                {
                    this.amount = desiredAmount;
                    return amount;
                }
            }
            else
                Debug.Log("You can't add negative amount of ammo. Use TakeAmmo instead");
            return 0;
        }

        /// <summary>
        /// takes ammo from invetory
        /// </summary>
        /// <param name="amountRequested">amount of ammo requested</param>
        /// <returns>returns amount of ammo taken from inventory</returns>
        public int TakeAmmo(int amountRequested)
        {
            if (amountRequested > 0)
            {
                int ammoTaken = Mathf.Min(amount, amountRequested);
                amount -= ammoTaken;
                return ammoTaken;
            }
            else
                Debug.Log("You can't take negative amount of ammo. Use AddAmmo instead");
            return 0;
        }
    }

    public Ammo[] fireArmAmmo;
    public Ammo[] explosiveAmmo;
    
    public GameObject[] explosiveArray; // add all types of grenades to array in inspector
    public GameObject activeGrenade;

    public GameObject[] weaponArray; // add all types of weapons to array in inspector
    private Weapon aWeaponScript;
    
    private int awAmmoType;
    private int aeAmmoType;

    public GameObject activeWeaponTip;
    public bool cooldownEnded = true;
    public bool isReloading = false;
    private SpriteRenderer spriteRenderer;
    private Allerting playerAlerting;
    private Animator playerAnimator;
    private int selectedFireArm = -1;
    private int selectedExplosive = -1;

    private GUIManager guiManager;
    private CameraScript mainCameraScript;
    private AudioSource audioSource;


    // Use this for initialization
    void Start ()
    {
        mainCameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        guiManager = GameObject.Find("GUI").GetComponent<GUIManager>();
        // static information about ofence weapons 
        fireArmAmmo = new Ammo[4];
        explosiveAmmo = new Ammo[2];
        GetAmmoFromMemory();
        ///

        playerAnimator = transform.parent.GetComponent<Animator>();
        playerAlerting = transform.parent.GetComponent<Allerting>();
        audioSource = transform.parent.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SwitchWeaponRight();
        SwitchExplosiveRight();

        // fill weapons with bullets on start
        foreach (GameObject w in weaponArray)
        {
            Weapon wcs = w.GetComponent<Weapon>();
            wcs.currentClipAmmo = wcs.clipSize;
        }

        UpdateWeaponGUI();
        UpdateExplosiveGUI();
        UpdateBulletGUI();
    }

    public void UpdateWeaponGUI() // update gui
    {
        for(int i = 0; i < weaponArray.Length; i++)
        {
            Weapon weaponData = weaponArray[i].GetComponent<Weapon>();
            if (weaponData.isDiscovered)
            {
                //we haven't gained this weapon yet
            }
            else if (weaponData.currentClipAmmo + fireArmAmmo[awAmmoType].amount > 0)
            {
                //we have weapon which is in index i
            }
            else
            {
                //we don't have weapon which is in index i
            }
        }
    }

    public void UpdateBulletGUI()
    {
        if(selectedFireArm == -1)
        {
            //display no ammo - infinity or whatevs when knife should be selected
        }
        else
        {
            guiManager.SetBulletGUI(aWeaponScript.currentClipAmmo, fireArmAmmo[awAmmoType].amount);
            // show how many bullets left 
            // weapon sprite to display next to bullets nums: weaponArray[selectedFireArm].GetComponent<SpriteRenderer>().sprite
            // bullets in gun: aWeaponScript.currentClipAmmo
            // bullets in inventory (pool): fireArmAmmo[awAmmoType].amount
        }
    }

    public void UpdateExplosiveGUI()
    {
        if (selectedExplosive == -1)
        {
            //display no explosives no image or whatevs
        }
        else
        {
            // show how many explosives left 
            // explosive sprite to display next to explosive nums: explosiveArray[selectedExplosive].GetComponent<SpriteRenderer>().sprite
            // explosives left: explosiveAmmo[aeAmmoType].amount
        }
    }

    public void GetAmmoFromMemory() //placeholder - should be changed with call to data saving and aquiring script
    {
        fireArmAmmo[0] = new Ammo
        {
            amount = 50,
            maxAmount = 120,
            name = "pistol"
        }; //pistol ammo
        fireArmAmmo[1] = new Ammo
        {
            amount = 5,
            maxAmount = 10,
            name = "rocket"
        }; //rocket launcher ammo

        fireArmAmmo[2] = new Ammo
        {
            amount = 120,
            maxAmount = 180,
            name = "assaultRifle"
        }; //assault rifle ammo

        fireArmAmmo[3] = new Ammo
        {
            amount = 24,
            maxAmount = 80,
            name = "shotgun"
        }; // shotgun ammo

        explosiveAmmo[0] = new Ammo
        {
            amount = 5,
            maxAmount = 8,
            name = "fragGrenade"
        }; // simple grande ammo

        explosiveAmmo[1] = new Ammo
        {
            amount = 5,
            maxAmount = 8,
            name = "gravnade"
        }; // simple grenade ammo
        //checking if keys for ammo exist and then assigning new ammo values
        for (int i = 0; i < fireArmAmmo.Length; i++)
        {
            if (PlayerPrefs.HasKey(fireArmAmmo[i].name + "Ammo"))
            {
                Debug.Log("loaded " + fireArmAmmo[i].name + PlayerPrefs.GetInt(fireArmAmmo[i].name + "Ammo"));
                fireArmAmmo[i].amount = PlayerPrefs.GetInt(fireArmAmmo[i].name + "Ammo");
            }
        }

        for (int i = 0; i < explosiveAmmo.Length; i++)
        {
            if (PlayerPrefs.HasKey(explosiveAmmo[i].name + "Ammo"))
            {
                explosiveAmmo[i].amount = PlayerPrefs.GetInt(explosiveAmmo[i].name + "Ammo");
            }
        }
    }

    private void UpdateWeapon()
    {
        if(selectedFireArm == -1)
        {
            //selected knife
        }
        else
        {
            aWeaponScript = weaponArray[selectedFireArm].GetComponent<Weapon>();
            activeWeaponTip.transform.localPosition = aWeaponScript.tip.transform.localPosition;
            aWeaponScript.Equip(gameObject);
            awAmmoType = aWeaponScript.ammoType;
            audioSource.clip = aWeaponScript.weaponSound;
        }
        UpdateBulletGUI();
    }

    private void UpdateExplosive()
    {
        if (selectedFireArm == -1)
        {
            //None explosives left
        }
        else
        {
            activeGrenade = explosiveArray[selectedExplosive];
            aeAmmoType = activeGrenade.GetComponent<Explosive>().AmmoType;
        }
        UpdateExplosiveGUI();
    }

    public void Reload()
    {
        if (!isReloading)
        {
            if (aWeaponScript.currentClipAmmo != aWeaponScript.clipSize && fireArmAmmo[awAmmoType].amount > 0)
                StartCoroutine(Reloadco());
        }
    }

    private IEnumerator Reloadco()
    {
        isReloading = true;
        playerAnimator.SetFloat("reloadTime", 1/aWeaponScript.reloadTime);
        playerAnimator.SetTrigger("playerReload");
        yield return new WaitForSeconds(aWeaponScript.reloadTime);

        int takenAmmo = fireArmAmmo[awAmmoType].TakeAmmo(aWeaponScript.clipSize - aWeaponScript.currentClipAmmo);
        aWeaponScript.currentClipAmmo += takenAmmo;
        UpdateBulletGUI();
        UpdateWeaponGUI();
        isReloading = false;
    }

    public void Shoot()
    {
        if(selectedFireArm == -1)
        {
            //knife attack
        }
        else
        {
            if (cooldownEnded && !isReloading)
            {
                if (aWeaponScript.currentClipAmmo > 0)
                {
                    cooldownEnded = false;
                    aWeaponScript.currentClipAmmo--;
                    UpdateBulletGUI();
                    StartCoroutine("Cooldown");
                    mainCameraScript.AddOffset(aWeaponScript.cameraRecoil);
                    playerAlerting.AllertSurroundings(aWeaponScript.allertingRadius);
                    audioSource.Play();

                    switch (selectedFireArm) ////////////requires optimisation - maybe code firing in weapon prefabs, or leave like this
                    {
                        case 0:
                            ShootPistol();
                            break;
                        case 1:
                            ShootRocket();
                            break;
                        case 2:
                            ShootAssaultRifle();
                            break;
                        case 3:
                            ShootShotgun();
                            break;
                    }

                    if (aWeaponScript.currentClipAmmo == 0)
                    {
                        Reload();
                        if (fireArmAmmo[awAmmoType].amount == 0)
                            UpdateWeaponGUI();
                    }
                }
                else
                {
                    Reload();
                }
            }
        }
    }

    private void ShootPistol()
    {
        GameObject newBullet = Instantiate(aWeaponScript.projectile, activeWeaponTip.transform.position, transform.rotation);
        newBullet.GetComponent<Bullet>().Instantiate(aWeaponScript.timeUntilSelfDestrucion, aWeaponScript.projectileSpeed, aWeaponScript.damage);
    }

    private void ShootRocket()
    {
        GameObject newRocket = Instantiate(aWeaponScript.projectile, activeWeaponTip.transform.position, transform.rotation);
        RocketLauncher rocketLauncher = weaponArray[selectedFireArm].GetComponent<RocketLauncher>();
        newRocket.GetComponent<GuidedMisile>().Instantiate(rocketLauncher.projectileSpeed, rocketLauncher.rotationSpeed, rocketLauncher.radius, rocketLauncher.force);
    }
    
    private void ShootAssaultRifle()
    {
        GameObject newBullet = Instantiate(aWeaponScript.projectile, activeWeaponTip.transform.position, transform.rotation);
        newBullet.GetComponent<Bullet>().Instantiate(aWeaponScript.timeUntilSelfDestrucion, aWeaponScript.projectileSpeed, aWeaponScript.damage);
    }

    private void ShootShotgun()
    {
        GameObject newBullet = Instantiate(aWeaponScript.projectile, activeWeaponTip.transform.position, transform.rotation);
        newBullet.GetComponent<Bullet>().Instantiate(aWeaponScript.timeUntilSelfDestrucion, aWeaponScript.projectileSpeed, aWeaponScript.damage);
    }

    //for explosives throwing
    public void UseExplosive()
    {
        if (!isReloading)
        {
            if (selectedExplosive == -1)
            {
                //no explosives left
            }
            else
            {
                if(explosiveAmmo[selectedExplosive].amount > 0)
                {
                    TakeExplosivesByIndex(selectedExplosive, 1);
                    Vector3 instantiatePos = transform.position;
                    Debug.Log("throw");
                    if (explosiveArray[selectedExplosive] != null)
                    {
                        //var nade = PrefabUtility.InstantiatePrefab(explosiveArray[selectedExplosive]) as GameObject;
                        GameObject nade = Instantiate(activeGrenade, instantiatePos, transform.rotation);
                        //nade.Throw(500);
                    }
                }
            }
        }
    }
    
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(aWeaponScript.cooldown);
        cooldownEnded = true;
    }


    // API:

        // API: Explosives manipulation

    public void SwitchExplosiveLeft()
    {
            int i = 0;
            for (; i < explosiveArray.Length; i++) // find explosive with ammo
            {
                selectedExplosive--;
                if (selectedExplosive < 0)
                    selectedExplosive = explosiveArray.Length - 1;
                // if any ammo left:
                if (explosiveAmmo[explosiveArray[selectedExplosive].GetComponent<Explosive>().AmmoType].amount > 0)
                    break;
            }
            if (i == explosiveArray.Length)
                selectedExplosive = -1;
            UpdateExplosive();
    }

    public void SwitchExplosiveRight()
    {
            int i = 0;
            for (; i < explosiveArray.Length; i++) // find explosive with ammo
            {
                selectedExplosive++;
                if (selectedExplosive >= explosiveArray.Length)
                    selectedExplosive = 0;
                // if any ammo left:
                if (explosiveAmmo[explosiveArray[selectedExplosive].GetComponent<Explosive>().AmmoType].amount > 0)
                    break;
            }
            if (i == explosiveArray.Length)
                selectedExplosive = -1;
            UpdateExplosive();
    }

    public void SelectExplosiveByIndex(int index)
    {
            if (index >= 0 && index < explosiveArray.Length)
            {
                selectedExplosive = index;
                UpdateExplosive();
            }
    }

    // API: Weapon manipulations

    public void SwitchWeaponLeft()
    {
        if (!isReloading)
        {
            int i = 0;
            for (; i < weaponArray.Length; i++) // find explosive with ammo
            {
                selectedFireArm--;
                if (selectedFireArm < 0)
                    selectedFireArm = weaponArray.Length - 1;
                // if any ammo left:
                if (fireArmAmmo[weaponArray[selectedFireArm].GetComponent<Weapon>().ammoType].amount > 0)
                    break;
            }
            if (i == weaponArray.Length)
                selectedFireArm = -1;
            UpdateWeapon();
        }
    }

    public void SwitchWeaponRight()
    {
        if (!isReloading)
        {
            int i = 0;
            for (; i < weaponArray.Length; i++) // find explosive with ammo
            {
                selectedFireArm++;
                if (selectedFireArm >= weaponArray.Length)
                    selectedFireArm = 0; 
                // if any ammo left:
                if (fireArmAmmo[weaponArray[selectedFireArm].GetComponent<Weapon>().ammoType].amount > 0)
                    break;
            }
            if (i == weaponArray.Length)
                selectedFireArm = -1;
            UpdateWeapon();
        }
    }

    public void SelectWeaponByIndex(int index)
    {
        if (!isReloading)
        {
            if(index >= 0 && index < weaponArray.Length)
            {
                selectedFireArm = index;
                UpdateWeapon();
            }
        }
    }

    // API: Ammo manipulations
    /// <summary>
    /// adds ammo by ammo name
    /// </summary>
    /// <param name="name">name of ammo to add</param>
    /// <param name="amount">amount of ammo to add</param>
    /// <returns>return amount added or -1 if such ammo type doesn't exist</returns>
    public int AddAmmoByName(string name, int amount)
    {
        foreach (Ammo ammo in fireArmAmmo)
            if (ammo.name == name)
            {
                int added = ammo.AddAmmo(amount);
                UpdateWeaponGUI();
                UpdateBulletGUI();
                return added;
            }
        foreach (Ammo ammo in explosiveAmmo)
            if (ammo.name == name)
            {
                int added = ammo.AddAmmo(amount);
                UpdateWeaponGUI();
                UpdateBulletGUI();
                return added;
            }
        return -1;
    }

    /// <summary>
    /// takes ammo by ammo name
    /// </summary>
    /// <param name="name">name of ammo to take</param>
    /// <param name="amount">amount of ammo to take</param>
    /// <returns>return amount taken or -1 if such ammo type doesn't exist</returns>
    public int TakeAmmoByName(string name, int amount)
    {
        foreach (Ammo ammo in fireArmAmmo)
            if (ammo.name == name)
            {
                int taken = ammo.TakeAmmo(amount);
                UpdateWeaponGUI();
                UpdateBulletGUI();
                return taken;
            }
        foreach (Ammo ammo in explosiveAmmo)
            if (ammo.name == name)
            {
                int taken = ammo.TakeAmmo(amount);
                UpdateWeaponGUI();
                UpdateBulletGUI();
                return taken;
            }
        return -1;
    }

    /// <summary>
    /// adds ammo by ammo index
    /// </summary>
    /// <param name="index">index of ammo to add</param>
    /// <param name="amount">amount of ammo to add</param>
    /// <returns>return amount added or -1 if such ammo doesn't exist</returns>
    public int AddAmmoByAmmoIndex(int index, int amount)
    {
        if (index >= 0 && index < fireArmAmmo.Length)
        {
            int added = fireArmAmmo[index].AddAmmo(amount);
            UpdateWeaponGUI();
            UpdateBulletGUI();
            return added;
        }
        return -1;
    }

    /// <summary>
    /// takes ammo by ammo index
    /// </summary>
    /// <param name="index">index of ammo to take</param>
    /// <param name="amount">amount of ammo to take</param>
    /// <returns>return amount taken or -1 if such ammo doesn't exist</returns>
    public int TakeAmmoByAmmoIndex(int index, int amount)
    {
        if (index >= 0 && index < fireArmAmmo.Length)
        {
            int taken = fireArmAmmo[index].TakeAmmo(amount);
            UpdateWeaponGUI();
            UpdateBulletGUI();
            return taken;
        }
        return -1;
    }

    /// <summary>
    /// takes ammo by weapon index
    /// </summary>
    /// <param name="index">index of weapon to add ammo</param>
    /// <param name="amount">amount of ammo to add</param>
    /// <returns>return amount added or -1 if such ammo doesn't exist</returns>
    public int AddAmmoByWeaponIndex(int index, int amount)
    {
        if (index >= 0 && index < weaponArray.Length)
        {
            int added = fireArmAmmo[weaponArray[index].GetComponent<Weapon>().ammoType].AddAmmo(amount);
            UpdateWeaponGUI();
            if (index == selectedFireArm)
                UpdateBulletGUI();
            return added;
        }
        return -1;
    }

    /// <summary>
    /// takes ammo by weapon index
    /// </summary>
    /// <param name="index">index of weapon to take ammo</param>
    /// <param name="amount">amount of ammo to take</param>
    /// <returns>return amount taken or -1 if such ammo doesn't exist</returns>
    public int TakeAmmoByWeaponIndex(int index, int amount)
    {
        if (index >= 0 && index < weaponArray.Length)
        {
            int taken = fireArmAmmo[weaponArray[index].GetComponent<Weapon>().ammoType].TakeAmmo(amount);
            UpdateWeaponGUI();
            if(index == selectedFireArm)
                UpdateBulletGUI();
            return taken;
        }
        return -1;
    }

    /// <summary>
    /// adds explosives by explosives index
    /// </summary>
    /// <param name="index">index of explosives to add</param>
    /// <param name="amount">amount of explosives to add</param>
    /// <returns>return amount added or -1 if such explosives doesn't exist</returns>
    public int AddExplosivesByIndex(int index, int amount)
    {
        if (index >= 0 && index < explosiveAmmo.Length)
        {
            int added = explosiveAmmo[index].AddAmmo(amount);
            UpdateExplosiveGUI();
            return added;
        }
        return -1;
    }

    /// <summary>
    /// takes explosives by explosives index
    /// </summary>
    /// <param name="index">index of explosives to take</param>
    /// <param name="amount">amount of explosives to take</param>
    /// <returns>return amount taken or -1 if such explosives doesn't exist</returns>
    public int TakeExplosivesByIndex(int index, int amount)
    {
        if(index >= 0 && index < explosiveAmmo.Length)
        {
            int taken = explosiveAmmo[index].TakeAmmo(amount);
            UpdateExplosiveGUI();
            return taken;
        }
        return -1;
    }
}
