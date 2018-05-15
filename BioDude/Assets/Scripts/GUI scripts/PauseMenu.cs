using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool PausemenuOpen = false;
    public static bool IsPlayerDead = false;
    public GameObject PauseMenuUI;
    public GameObject PauseMenuPanel;
    public GameObject OptionsMenuPanel;
    public GameObject GameOverMenu;
    public DialogueManager DialManager;
    public GameObject DeathSplashImage;

    private float time;
    void Start()
    {
        PauseMenuUI.SetActive(false);  //disabling pausemenu canvas because it should only be active when pausemenu is summoned
        ResetPanels();
        IsPlayerDead = false;

    }
    public void ResetPanels()
    {
        PauseMenuPanel.SetActive(true);
        OptionsMenuPanel.SetActive(false);
        GameOverMenu.SetActive(false);
        DeathSplashImage.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if(IsPlayerDead)
        {

        }
        else
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausemenuOpen)
                Resume();
            else if(!DialManager.IsDialogueOpen() && !IsPlayerDead)
                Pause();
        }
	}

    public void OnEnable()
    {
        ResetPanels();
    }
    public void Pause()
    {
        DialManager.SetDialogueState(false);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PausemenuOpen = true;
        ResetPanels();
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PausemenuOpen = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator PlayerDeath()
    {
        IsPlayerDead = true;
        yield return new WaitForSeconds(1);
        PauseMenuUI.SetActive(true);
        ResetPanels();
        PauseMenuPanel.SetActive(false);
        PauseMenuUI.GetComponent<Animator>().Play("PausemenuFade");
        DeathSplashImage.SetActive(true);
        DeathSplashImage.GetComponent<Animator>().Play("DeathImageSplash");
        yield return new WaitForSeconds(3);
        //Time.timeScale = 0;   //comment/uncomment to disable/enable the flow of time
        GameOverMenu.SetActive(true);
    }


}
