using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{

    private string name;

    private string description;

    private bool unlocked;

    private int points;

    private int spriteIndex;

    private GameObject achievementRef;

    private List<Achievement> dependencies = new List<Achievement>();

    private string child;

    private int currentProgression;
    private int maxProgression;

    public Achievement(string name, string description, int points, int spriteIndex, GameObject achievementRef, int maxProgression)
    {
        this.name = name;
        this.description = description;
        this.Unlocked = false;
        this.points = points;
        this.spriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
        this.maxProgression = maxProgression;


        LoadAchievement();
    }

    public void AddDependency(Achievement dependency)
    {
        dependencies.Add(dependency);
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public bool Unlocked
    {
        get
        {
            return unlocked;
        }

        set
        {
            unlocked = value;
        }
    }

    public int Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
        }
    }

    public GameObject AchievementRef
    {
        get
        {
            return achievementRef;
        }

        set
        {
            achievementRef = value;
        }
    }

    public int SpriteIndex
    {
        get
        {
            return spriteIndex;
        }

        set
        {
            spriteIndex = value;
        }
    }

    public string Child
    {
        get
        {
            return child;
        }

        set
        {
            child = value;
        }
    }

    public bool EarnAchievement()
    {
        if (!Unlocked && !dependencies.Exists(x => x.unlocked == false) && CheckProgress())
        {
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
            SaveAchievement(true);

            if (child != null)
            {
                AchievementManager.Instance.EarnAchievement(child);
            }
            return true;
        }
        return false;
    }

    public void SaveAchievement(bool value)
    {
        unlocked = value;


        if (value == true)
        {
            int tmpPoints = PlayerPrefs.GetInt("Points");

            PlayerPrefs.SetInt("Points", tmpPoints += points);
            PlayerPrefs.SetInt(name, 1);
        }
        else
        {
            PlayerPrefs.SetInt(name, 0);
        }

        PlayerPrefs.SetInt("Progression" + Name, currentProgression);



        PlayerPrefs.Save();
        //stores achievement's status
        PlayerPrefs.SetInt(name, value ? 1 : 0);

    }

    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;

        if (unlocked)
        {
            AchievementManager.Instance.textPoints.text = "Points: " + PlayerPrefs.GetInt("Points").ToString();
            currentProgression = PlayerPrefs.GetInt("Progression" + Name);
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
        }
    }

    public bool CheckProgress()
    {
        currentProgression++;
        if (maxProgression != 0)
        achievementRef.transform.GetChild(0).GetComponent<Text>().text = Name + " " + currentProgression + "/" + maxProgression;

        SaveAchievement(false);

        if (maxProgression == 0)
        {
            return true;
        }
        if (currentProgression >= maxProgression)
        {
            return true;
        }

        return false;
    }
    
}
