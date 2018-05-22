using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{

    public GameObject[] ObjectToMove;

    public GameObject menu;

    private void Awake()
    {
        foreach (var item in ObjectToMove)
        {
            DontDestroyOnLoad(item);
        }Time.timeScale = 1;
    }

    public void LoadByIndex(int sceneIndex)
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
