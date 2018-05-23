using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MatynasTest {

    WeaponManager WeaponManager;
    GameObject bullet;

    [UnityTest]
    public IEnumerator BulletFlyDistance()
    {
        Debug.Log("before");
        int time = 1;
        float expdist = WeaponManager.weaponArray[0].GetComponent<Weapon>().projectileSpeed * time;
        Vector2 startingpos = bullet.transform.position;
        Debug.Log(expdist);
        yield return new WaitForSecondsRealtime(time);
        Debug.Log("after");
        Vector2 endingpos = bullet.transform.position;
        //Assert.AreEqual(10, Vector2.Distance(startingpos, endingpos), "Wrong distance");
    }
    [UnityTest]
    public IEnumerator BulletDestroyed()
    {
        yield return new WaitForSecondsRealtime(WeaponManager.weaponArray[0].GetComponent<Weapon>().timeUntilSelfDestrucion);
        //Assert.AreEqual(null, bullet, "Bullet not destroyed");
    }
}
