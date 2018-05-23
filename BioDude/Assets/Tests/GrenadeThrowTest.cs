using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

public class GrenadeThrowTest {
    
	[Test]
    public void GrenadeTests()
    {
        GameObject ToBeThrownObject = null;
        //test1
        ToBeThrownObject = Resources.Load<GameObject>("fragGrenade");
        Assert.NotNull(ToBeThrownObject);
        SpawnGrenade(ToBeThrownObject);
        //test2
        ToBeThrownObject = Resources.Load<GameObject>("gravnade");
        Assert.NotNull(ToBeThrownObject);
        SpawnGrenade(ToBeThrownObject);
    }
    public void SpawnGrenade(GameObject grenade) {
        Assert.NotNull(grenade);
        var objectlauncher = new GameObject().AddComponent<WeaponManager>();
        objectlauncher.activeGrenade = grenade;
        var obj = objectlauncher.UseActiveGrenade();
        //var spawnedObject = GameObject.FindWithTag("Explosive");
        //var ThrownObject = PrefabUtility.GetPrefabParent(spawnedObject);
        var ThrownObject = PrefabUtility.GetPrefabParent(obj);

        Assert.NotNull(ThrownObject);
        Debug.Log(grenade + "==" + ThrownObject);
        Assert.AreEqual(grenade, ThrownObject);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
	public IEnumerator GrenadeThrowTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return new WaitForSeconds(1);
    }
    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameObject in GameObject.FindObjectsOfType<Explosive>())
        {
            Object.Destroy(gameObject);
        }
        foreach (var gameObject in Object.FindObjectsOfType<player>())
        {
            Object.Destroy(gameObject);
        }

    }
}