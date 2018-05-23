using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class AidasTestas
{

    AchievementManager achievementManager;
    GameObject achievementPanel;

    [Test]
    public void AchievementsDestroyTest()
    {
        Assert.DoesNotThrow(() => { achievementManager.DestroyAllAchievements(); }, "Achievements should have been destroyed, this is an error");
    }

    [Test]
    public void LoadAchievementPanelTest()
    {
        Assert.IsNotNull(achievementPanel, "Achievement manager has not been yet created");
    }

    [Test]
    public void LoadAchievementManagerTest()
    {
        Assert.IsNotNull(achievementManager, "Achievement manager has not been yet created");
    }

    [SetUp]
    public void AidasSetUp()
    {
        achievementPanel = Resources.Load<GameObject>("AchievementPanel");
        GameObject dummyObject = Resources.Load<GameObject>("Achievements");
        GameObject visualAchievement = Resources.Load<GameObject>("VisualAchievement");
        achievementManager = dummyObject.transform.Find("AchievementManager").GetComponent<AchievementManager>();
    }
}