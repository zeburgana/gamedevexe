using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroSceneLoadMenu : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        StartCoroutine(WaitForVideoEnd());
    }

    private static IEnumerator WaitForVideoEnd()
    {
        double length = GameObject.FindGameObjectWithTag("GameController").GetComponent<VideoPlayer>().clip.length;
        
        yield return new WaitForSeconds((float) length);

        LoadNextLevel();
    }

    private static void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}