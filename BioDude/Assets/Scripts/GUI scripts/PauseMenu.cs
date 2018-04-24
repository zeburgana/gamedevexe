using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool PausemenuOpen = false;
    public GameObject PauseMenuUI;
    public GameObject PauseMenuPanel;
    public GameObject OptionsMenuPanel;
    public DialogueManager DialManager;
    private float time;
    void Start()
    {
        PauseMenuUI.SetActive(false);
        PauseMenuPanel.SetActive(true);
        OptionsMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausemenuOpen)
                Resume();
            else if(!DialManager.IsDialogueOpen())
                Pause();
        }
	}
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        //Time.timeScale = time;
        Time.timeScale = 1f;
        PausemenuOpen = false;
    }
    void Pause()
    {
        DialManager.SetDialogueState(false);
        PauseMenuUI.SetActive(true);
        //time = Time.timeScale;
        Time.timeScale = 0f;
        PausemenuOpen = true;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        //Time.timeScale = 1f;
        Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
