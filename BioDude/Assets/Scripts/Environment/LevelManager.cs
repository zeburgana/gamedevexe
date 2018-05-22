using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    int EnemiesOnMapLeft = 0;
    string LastLevelKeyName = "LastLevelCheckpoint";
	// Use this for initialization
    void Start()
    {
        if(GameObject.Find("Enemies") != null)
            EnemiesOnMapLeft = GameObject.Find("Enemies").transform.childCount;
        if (SceneManager.GetActiveScene().buildIndex > 0 &&
            SceneManager.GetActiveScene().name != "Menu")
            SaveCurrentLevelIndex();
    }

    public void LevelCleared()
    {
        //play level finished screen with option to load next level
        Debug.Log("Stage cleared");
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex +1;
        PlayerPrefs.SetInt(LastLevelKeyName, nextSceneIndex);
        SceneManager.LoadScene(nextSceneIndex);
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
