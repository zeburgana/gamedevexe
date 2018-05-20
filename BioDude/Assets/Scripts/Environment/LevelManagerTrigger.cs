using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerTrigger : MonoBehaviour {
    LevelManager lvlManager;

    void Start()
    {
        lvlManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
	// Use this for initialization
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player Player = other.GetComponent<player>();
            if(Player != null)
            {
                Player.SavePlayerStats();
                lvlManager.LoadNextLevel();
            }
            else
                Debug.Log("Character script not found on player");
        }
    }
}
