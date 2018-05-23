using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

public class GrenadeThrowTest {

	[Test]
	public void GrenadeThrowTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator GrenadeThrowTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject ToBeThrownObject = Resources.Load<GameObject>("fragGrenade");
        Assert.NotNull(ToBeThrownObject);
        var objectlauncher = new GameObject().AddComponent<WeaponManager>();
        objectlauncher.activeGrenade = ToBeThrownObject;
        var obj = objectlauncher.UseActiveGrenade();
        //var spawnedObject = GameObject.FindWithTag("Explosive");
        //var ThrownObject = PrefabUtility.GetPrefabParent(spawnedObject);
        var ThrownObject = PrefabUtility.GetPrefabParent(obj);

        Assert.NotNull(ThrownObject);
        Debug.Log(ToBeThrownObject + "==" + ThrownObject);
        Assert.AreEqual(ToBeThrownObject, ThrownObject);
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