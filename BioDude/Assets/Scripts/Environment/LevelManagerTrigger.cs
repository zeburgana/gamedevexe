using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerTrigger : MonoBehaviour {
    public LevelManager lvlManager;
    void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = null;
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
