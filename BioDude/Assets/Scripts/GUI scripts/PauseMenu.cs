﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool PausemenuOpen = false;
    public GameObject PauseMenuUI;
    public GameObject PauseMenuPanel;
    public GameObject OptionsMenuPanel;
    public GameObject GameOverMenu;
    public DialogueManager DialManager;
    private float time;
    void Start()
    {
        PauseMenuUI.SetActive(false);  //disabling pausemenu canvas because it should only be active when pausemenu is summoned
        ResetPanels();
    }
    public void ResetPanels()
    {
        PauseMenuPanel.SetActive(true);
        OptionsMenuPanel.SetActive(false);
        GameOverMenu.SetActive(false);
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
        Time.timeScale = 1f;
        PausemenuOpen = false;
    }
    void Pause()
    {
        DialManager.SetDialogueState(false);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PausemenuOpen = true;
        ResetPanels();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Resume();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
