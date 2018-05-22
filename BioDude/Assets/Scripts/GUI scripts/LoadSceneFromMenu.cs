using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneFromMenu : MonoBehaviour
{

    public GameObject[] ObjectToMove;
    
    public AchievementManager achievementManager;
    public GameObject achievementPanel;

    private void Awake()
    {
        foreach (var item in ObjectToMove)
        {
            DontDestroyOnLoad(item);
        }
        Time.timeScale = 1;
    }
    public void NewGame()
    {
        achievementManager.DestroyAllAchievements();
        Destroy(GameObject.Find("MainMenuCanvas"));

        DeletePlayerProgress();
        LoadByIndex(1);
    }
    public void ContinueGame()
    {
        int indexToLoad = PlayerPrefs.GetInt("LastLevelCheckpoint");
        Destroy(GameObject.Find("MainMenuCanvas"));
        LoadByIndex(indexToLoad);
    }

    void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1f;
    }
    public void DeletePlayerProgress()
    {
        PlayerPrefs.DeleteKey("LastLevelCheckpoint");
        PlayerPrefs.DeleteKey("PlayerHP");
        PlayerPrefs.DeleteKey("pistolAmmo");
        PlayerPrefs.DeleteKey("shotgunAmmo");
        PlayerPrefs.DeleteKey("assaultRifleAmmo");
        PlayerPrefs.DeleteKey("rocketAmmo");
        PlayerPrefs.DeleteKey("fragGrenadeAmmo");
        PlayerPrefs.DeleteKey("gravnadeAmmo");

    }
}
