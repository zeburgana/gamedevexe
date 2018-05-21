using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    int EnemiesOnMapLeft = 0;
    PauseMenu Pausemenu;
    string LastLevelKeyName = "LastLevelCheckpoint";
	// Use this for initialization
    void Start()
    {
        GameObject obj = GameObject.Find("Pausemenu Canvas");
        Debug.Log(obj);
        Pausemenu = obj.GetComponent<PauseMenu>();
        Debug.Log(Pausemenu);

        if(GameObject.Find("Enemies") != null)
            EnemiesOnMapLeft = GameObject.Find("Enemies").transform.childCount;
        if (SceneManager.GetActiveScene().buildIndex > 0 &&
            SceneManager.GetActiveScene().name != "Menu")
            SaveCurrentLevelIndex();
    }

    public void LevelCleared()
    {
        //play level finished screen with option to load next level
        Pausemenu.ShowNextLevelScreen();
        Debug.Log("Stage cleared");
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex +1;
        PlayerPrefs.SetInt(LastLevelKeyName, nextSceneIndex);
        Debug.Log("load");
        if (nextSceneIndex > -1 && nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextSceneIndex);
        else
            Debug.Log("cannot load scene: " + nextSceneIndex);
    }
    public int GetLastLevelIndex()
    {
        if (PlayerPrefs.HasKey(LastLevelKeyName))
            return PlayerPrefs.GetInt(LastLevelKeyName);
        else
            return -1;
    }
    public void SaveCurrentLevelIndex()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(LastLevelKeyName, currentSceneIndex);
        PlayerPrefs.Save();
    }

    public bool DoesPlayerProgressExist()
    {
        return PlayerPrefs.HasKey(LastLevelKeyName);
    }

    public void EnemyDefeated()
    {
        EnemiesOnMapLeft--;
        if (EnemiesOnMapLeft == 0)
            LevelCleared();
    }
}
