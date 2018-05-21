using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneFromMenu : MonoBehaviour
{

    public GameObject[] ObjectToMove;

    public GameObject menu;

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
        DeletePlayerProgress();
        LoadByIndex(1);
    }
    public void ContinueGame()
    {
        int indexToLoad = PlayerPrefs.GetInt("LastLevelCheckpoint");
        LoadByIndex(indexToLoad);
    }

    void LoadByIndex(int sceneIndex)
    {
        ObjectToMove[0].transform.GetChild(0).GetComponent<Text>().text = "";
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1f;
    }
    public void DeletePlayerProgress()
    {
        PlayerPrefs.DeleteKey("LastLevelCheckpoint");
        PlayerPrefs.DeleteKey("PlayerHP");
        PlayerPrefs.DeleteKey("pistolAmmo");
        //PlayerPrefs.DeleteKey("PlayerShotgun");
        //PlayerPrefs.DeleteKey("PlayerRifle");
        PlayerPrefs.DeleteKey("rocketAmmo");
        PlayerPrefs.DeleteKey("fragGrenadeAmmo");
        PlayerPrefs.DeleteKey("gravnadeAmmo");

    }
}
