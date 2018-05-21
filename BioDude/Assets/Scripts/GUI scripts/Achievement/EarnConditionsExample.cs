using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EarnConditionsExample : MonoBehaviour {

    public string achievementName;



    /// <summary>
    /// 
    /// Achievements must be written in AchievementManager script Start() function
    /// 
    /// </summary>

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        //adds achievement when clicked
        if (!EventSystem.current.IsPointerOverGameObject(-1))
        {
            AchievementManager.Instance.EarnAchievement(achievementName);
        }
    }
}
