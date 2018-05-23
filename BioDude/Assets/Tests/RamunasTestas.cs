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
        Assert.AreEqual(expectedBullet.tag, bullet.tag, "Bullet failed {0} ", bullet.transform.position.ToString());
    }

    //[UnityTest]
    //public IEnumerator RTestWithEnumeratorPasses()
    //{
    //    WeaponManager.ShootPistol();
    //    Debug.Log("before");
    //    yield return new WaitForSecondsRealtime(8);
    //    Debug.Log("after");
    //}

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
        Object.Destroy(bullet);
    }
    
}

//public class WeaponEquipTests
//{

//    [UnityTest]
//    public IEnumerator WeaponEquipTestWithEnumeratorPasses()
//    {
//        var ToBeThrownObject = Resources.Load("Testing/grenade");
//        var objectlauncher = new GameObject().AddComponent<player>();
//        objectlauncher.TestSetSelectedGrenadeTo(ToBeThrownObject);
//        objectlauncher.GetComponent<player>().UseGrenade();
//        //yield return new WaitForSeconds(1);
//        var spawnedObject = GameObject.FindWithTag("Explosive");
//        Debug.Log(spawnedObject);
//        var ThrownObject = PrefabUtility.GetPrefabParent(spawnedObject);
//        Debug.Log(ThrownObject);
//        Assert.AreEqual(ToBeThrownObject, ThrownObject);
//        yield return new WaitForSeconds(1);
//    }

//    [TearDown]
//    public void AfterEveryTest()
//    {
//        foreach (var gameObject in GameObject.FindObjectsOfType<Explosive>())
//        {
//            Object.Destroy(gameObject);
//        }
//        foreach (var gameObject in Object.FindObjectsOfType<player>())
//        {
//            Object.Destroy(gameObject);
//        }

//    }

//}