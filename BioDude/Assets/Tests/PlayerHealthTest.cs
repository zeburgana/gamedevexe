using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerHealthTest {


    [UnityTest]
    public IEnumerator PlayerHealthShouldBeEqualTo85f() {
        Debug.Log("******* PlayerHealthShouldBeEqualTo85f() *******");
        Debug.Log("Starting the test");
        GameObject dummyObject = Resources.Load<GameObject>("player");
        player playerDummy = dummyObject.GetComponent<player>();
        playerDummy.healthMax = 100f;
        playerDummy.SetMaxHealth();
        Debug.Log("Initial player health: " + playerDummy.healthCurrent);
        Debug.Log("Damage amount to be given: 15f");
        playerDummy.Damage(15f);
        Debug.Log("Player health after damage: " + playerDummy.healthCurrent);
        Assert.AreEqual(85f, playerDummy.healthCurrent);
        Debug.Log("The test is done. Please check whether the test has failed or not");
        Debug.Log("*******************************************");
        Debug.Log("");
        playerDummy.Damage(85f);
        if (playerDummy.healthCurrent <= 0)
            playerDummy.Die();
        yield return new WaitForSeconds(1);
    }
    [UnityTest]
    public IEnumerator PlayerHealthShouldBeEqualTo85()
    {
        Debug.Log("******* PlayerHealthShouldBeEqualTo85() *******");
        Debug.Log("Starting the test");
        GameObject dummyObject = Resources.Load<GameObject>("player");
        player playerDummy = dummyObject.GetComponent<player>();
        playerDummy.healthMax = 100;
        playerDummy.SetMaxHealth();
        Debug.Log("Initial player health: " + playerDummy.healthCurrent);
        Debug.Log("Damage amount to be given: 15");
        playerDummy.Damage(15);
        Debug.Log("Player health after damage: " + playerDummy.healthCurrent);
        Assert.AreEqual(85, playerDummy.healthCurrent);
        Debug.Log("The test is done. Please check whether the test has failed or not");
        Debug.Log("*******************************************");
        Debug.Log("");
        playerDummy.Damage(85);
        if (playerDummy.healthCurrent <= 0)
            playerDummy.Die();
        yield return new WaitForSeconds(1);
    }

    [UnityTest]
    public IEnumerator PlayerHealthShouldntBeEqualToNegative5f()
    {
        Debug.Log("******* PlayerHealthShouldntBeEqualToNegative5f() *******");
        Debug.Log("Starting the test");
        GameObject dummyObject = Resources.Load<GameObject>("player");
        player playerDummy = dummyObject.GetComponent<player>();
        playerDummy.healthMax = 100f;
        playerDummy.SetMaxHealth();
        Debug.Log("Initial player health: " + playerDummy.healthCurrent);
        Debug.Log("Damage amount to be given: 105f");
        playerDummy.Damage(105f);
        Debug.Log("Player health after damage: " + playerDummy.healthCurrent);
        Assert.AreNotEqual(-5f, playerDummy.healthCurrent);
        Debug.Log("The test is done. Please check whether the test has failed or not");
        Debug.Log("*******************************************");
        Debug.Log("");
        if (playerDummy.healthCurrent <= 0)
            playerDummy.Die();
        yield return new WaitForSeconds(1);
    }

    [UnityTest]
    public IEnumerator PlayerHealthShouldntBeEqualToNegative5()
    {
        Debug.Log("******* PlayerHealthShouldntBeEqualToNegative5() *******");
        Debug.Log("Starting the test");
        GameObject dummyObject = Resources.Load<GameObject>("player");
        player playerDummy = dummyObject.GetComponent<player>();
        playerDummy.healthMax = 100;
        playerDummy.SetMaxHealth();
        Debug.Log("Initial player health: " + playerDummy.healthCurrent);
        Debug.Log("Damage amount to be given: 105");
        playerDummy.Damage(105);
        Debug.Log("Player health after damage: " + playerDummy.healthCurrent);
        Assert.AreNotEqual(-5, playerDummy.healthCurrent);
        Debug.Log("The test is done. Please check whether the test has failed or not");
        Debug.Log("*******************************************");
        Debug.Log("");
        if (playerDummy.healthCurrent <= 0)
            playerDummy.Die();
        yield return new WaitForSeconds(1);
    }

    [UnityTest]
    public IEnumerator PlayerHealthShouldBeEqualTo0f()
    {
        Debug.Log("******* PlayerHealthShouldBeEqualTo0f() *******");
        Debug.Log("Starting the test");
        GameObject dummyObject = Resources.Load<GameObject>("player");
        player playerDummy = dummyObject.GetComponent<player>();
        playerDummy.healthMax = 100f;
        playerDummy.SetMaxHealth();
        Debug.Log("Initial player health: " + playerDummy.healthCurrent);
        Debug.Log("Damage amount to be given: 100f");
        playerDummy.Damage(100f);
        Debug.Log("Player health after damage: " + playerDummy.healthCurrent);
        Assert.AreEqual(0f, playerDummy.healthCurrent);
        Debug.Log("The test is done. Please check whether the test has failed or not");
        Debug.Log("*******************************************");
        Debug.Log("");
        if (playerDummy.healthCurrent <= 0)
            playerDummy.Die();
        yield return new WaitForSeconds(1);
    }

    [UnityTest]
    public IEnumerator PlayerHealthShouldBeEqualTo0()
    {
        Debug.Log("******* PlayerHealthShouldBeEqualTo0() *******");
        Debug.Log("Starting the test");
        GameObject dummyObject = Resources.Load<GameObject>("player");
        player playerDummy = dummyObject.GetComponent<player>();
        playerDummy.healthMax = 100;
        playerDummy.SetMaxHealth();
        Debug.Log("Initial player health: " + playerDummy.healthCurrent);
        Debug.Log("Damage amount to be given: 100");
        playerDummy.Damage(100);
        Debug.Log("Player health after damage: " + playerDummy.healthCurrent);
        Assert.AreEqual(0, playerDummy.healthCurrent);
        Debug.Log("The test is done. Please check whether the test has failed or not");
        Debug.Log("*******************************************");
        Debug.Log("");
        if (playerDummy.healthCurrent <= 0)
            playerDummy.Die();
        yield return new WaitForSeconds(1);
    }

    [UnityTest]
    public IEnumerator PlayerMaxHealthSetTo0NoDamage()
    {
        Debug.Log("******* PlayerMaxHealthSetTo0NoDamage() *******");
        Debug.Log("Starting the test");
        GameObject dummyObject = Resources.Load<GameObject>("player");
        player playerDummy = dummyObject.GetComponent<player>();
        playerDummy.healthMax = 0;
        playerDummy.SetMaxHealth();
        Debug.Log("Initial player health: " + playerDummy.healthCurrent);
        Debug.Log("Damage amount to be given: 0");
        playerDummy.Damage(0);
        Debug.Log("Player health after damage: " + playerDummy.healthCurrent);
        Assert.AreEqual(0, playerDummy.healthCurrent);
        Debug.Log("The test is done. Please check whether the test has failed or not");
        Debug.Log("*******************************************");
        Debug.Log("");
        if (playerDummy.healthCurrent <= 0)
            playerDummy.Die();
        yield return new WaitForSeconds(1);
    }

    [UnityTest]
    public IEnumerator PlayerMaxHealthSetTo0WithDamage()
    {
        Debug.Log("******* PlayerMaxHealthSetTo0WithDamage() *******");
        Debug.Log("Starting the test");
        GameObject dummyObject = Resources.Load<GameObject>("player");
        player playerDummy = dummyObject.GetComponent<player>();
        playerDummy.healthMax = 0;
        playerDummy.SetMaxHealth();
        Debug.Log("Initial player health: " + playerDummy.healthCurrent);
        Debug.Log("Damage amount to be given: 50");
        playerDummy.Damage(50);
        Debug.Log("Player health after damage: " + playerDummy.healthCurrent);
        Assert.AreEqual(0, playerDummy.healthCurrent);
        Debug.Log("The test is done. Please check whether the test has failed or not");
        Debug.Log("*******************************************");
        Debug.Log("");
        if (playerDummy.healthCurrent <= 0)
            playerDummy.Die();
        yield return new WaitForSeconds(1);
    }

    [UnityTest]
    public IEnumerator PlayerMaxHealthSetTo0Overheal()
    {
        Debug.Log("******* PlayerMaxHealthSetTo0Overheal() *******");
        Debug.Log("Starting the test");
        GameObject dummyObject = Resources.Load<GameObject>("player");
        player playerDummy = dummyObject.GetComponent<player>();
        playerDummy.healthMax = 0;
        playerDummy.SetMaxHealth();
        Debug.Log("Initial player health: " + playerDummy.healthCurrent);
        playerDummy.Heal(50);
        Debug.Log("Heal amount to be given: 50");
        Debug.Log("Player health after heal: " + playerDummy.healthCurrent);
        Assert.AreEqual(0, playerDummy.healthCurrent);
        Debug.Log("The test is done. Please check whether the test has failed or not");
        Debug.Log("*******************************************");
        Debug.Log("");
        if (playerDummy.healthCurrent <= 0)
            playerDummy.Die();
        yield return new WaitForSeconds(1);
    }

    [UnityTest]
    public IEnumerator PlayerMaxHealthSetTo100Overheal()
    {
        Debug.Log("******* PlayerMaxHealthSetTo100Overheal() *******");
        Debug.Log("Starting the test");
        GameObject dummyObject = Resources.Load<GameObject>("player");
        player playerDummy = dummyObject.GetComponent<player>();
        playerDummy.healthMax = 100;
        playerDummy.SetMaxHealth();
        Debug.Log("Initial player health: " + playerDummy.healthCurrent);
        playerDummy.Heal(1500);
        Debug.Log("Heal amount to be given: 1500");
        Debug.Log("Player health after heal: " + playerDummy.healthCurrent);
        Assert.AreEqual(100, playerDummy.healthCurrent);
        Debug.Log("The test is done. Please check whether the test has failed or not");
        Debug.Log("*******************************************");
        Debug.Log("");
        if (playerDummy.healthCurrent <= 0)
            playerDummy.Die();
        yield return new WaitForSeconds(1);
    }


    [OneTimeTearDown]
    public void AfterEveryTest()
    {
        foreach (var gameObject in GameObject.FindObjectsOfType<player>())
        {
            Object.DestroyImmediate(gameObject);
        }
    }
}

