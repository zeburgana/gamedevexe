using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

public class WeaponEquipTests {

	[Test]
	public void WeaponEquipTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator WeaponEquipTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        var ToBeThrownObject = Resources.Load("Testing/grenade");
        var objectlauncher = new GameObject().AddComponent<player>();
        objectlauncher.TestSetSelectedGrenadeTo(ToBeThrownObject);
        objectlauncher.GetComponent<player>().UseGrenade();
        //yield return new WaitForSeconds(1);
        var spawnedObject = GameObject.FindGameObjectWithTag("Explosive");
        Debug.Log(spawnedObject);
        var ThrownObject = PrefabUtility.GetPrefabParent(spawnedObject);
        Debug.Log(ThrownObject);
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
