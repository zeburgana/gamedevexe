using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class RamunasTestas {

    GameObject bullet;
    GameObject expectedBullet;
    WeaponManager WeaponManager;

    [Test]
    public void BulletCreated()
    {
        Assert.IsNotNull(bullet, "Object was not instantiated");
    }

    [Test]
    public void InstatiatedWithTag()
    {
        Assert.AreEqual(expectedBullet.tag, bullet.tag, "Wrong Bullet Tag {0} ", bullet.transform.position.ToString());
    }

    [Test]
    public void rigidbodyTest() {
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        Rigidbody2D expected = expectedBullet.GetComponent<Rigidbody2D>();
        Assert.AreEqual(expected.angularDrag, rigidbody2D.angularDrag, "Wrong Angular Drag");
        Assert.AreEqual(expected.angularVelocity, rigidbody2D.angularVelocity, "Wrong Angular Velocity");
        Assert.AreEqual(expected.collisionDetectionMode, rigidbody2D.collisionDetectionMode, "Wrong collisionDetectionMode");
        Assert.AreEqual(expected.constraints, rigidbody2D.constraints, "Wrong constrains");
        Assert.AreEqual(expected.gravityScale, rigidbody2D.gravityScale, "Wrong gravity scale");
        Assert.AreEqual(expected.isKinematic, rigidbody2D.isKinematic, "Wrong is Kinematic");
    }

    [Test]
    public void bulletDamageChaged ()
    {
        Assert.AreNotEqual(expectedBullet.GetComponent<Bullet>().damage, bullet.GetComponent<Bullet>().damage, "Wrong Bullet damage");
    }

    [Test]
    public void BulletDamageSet()
    {
        Assert.AreEqual(bullet.GetComponent<Bullet>().damage, WeaponManager.weaponArray[0].GetComponent<Weapon>().damage, "Wrong damage set");
    }

    [Test]
    public void randomRotationgained()
    {
        Assert.GreaterOrEqual(bullet.transform.rotation.z,- WeaponManager.weaponArray[0].GetComponent<Weapon>().accuracy + WeaponManager.transform.rotation.z, "Wring rotation");
        Assert.LessOrEqual(bullet.transform.rotation.z, WeaponManager.weaponArray[0].GetComponent<Weapon>().accuracy+ WeaponManager.transform.rotation.z, "Wring rotation");
    }

    [SetUp]
    public void RamunasSetUp()
    {
        GameObject dummyObject = Resources.Load<GameObject>("player");
        WeaponManager = dummyObject.GetComponent<WeaponManager>();
        WeaponManager.selectedFireArm = 0;
        WeaponManager.activeWeaponRTip = WeaponManager.rightHandSlot.transform.GetChild(0).gameObject;
        WeaponManager.activeWeaponRTip.transform.localPosition = WeaponManager.weaponArray[0].GetComponent<Weapon>().tip.transform.localPosition;

        WeaponManager.ShootPistol();
        bullet = GameObject.Find("PistolBullet(Clone)");
        //bullet = GameObject.Find("PistolBullet");
        expectedBullet = Resources.Load<GameObject>("PistolBullet");
    }

    [TearDown] 
    public void RamunasTearDown()
    {
        Debug.Log("teardown");
        Object.Destroy(bullet);
    }
    
}


//[UnityTest]
//public IEnumerator BulletFlyDistance()
//{
//    Debug.Log("before");
//    int time = 1;
//    float expdist = WeaponManager.weaponArray[0].GetComponent<Weapon>().projectileSpeed * time;
//    Vector2 startingpos = bullet.transform.position;
//    Debug.Log(expdist);
//    yield return new WaitForSecondsRealtime(time);
//    Debug.Log("after");
//    Vector2 endingpos = bullet.transform.position;
//    Assert.AreEqual(expdist, Vector2.Distance(startingpos, endingpos), "Wrong distance");
//}
//[UnityTest]
//public IEnumerator BulletDestroyed()
//{
//    yield return new WaitForSecondsRealtime(WeaponManager.weaponArray[0].GetComponent<Weapon>().timeUntilSelfDestrucion);
//    Assert.AreEqual(< null >, bullet, "Bullet not destroyed");
//}
