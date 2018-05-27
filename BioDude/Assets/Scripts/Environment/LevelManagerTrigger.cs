using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerTrigger : MonoBehaviour {

    public float rotationSpeed = 10;
    public Dialogue[] dialoguetext;
    DialogueColliderTrigger dialogue;
    LevelManager lvlManager;
    void Start()
    {
        dialogue = new DialogueColliderTrigger();
        dialogue.dialogue = dialoguetext;
        //transform.GetComponent<SpriteRenderer>().sprite = null;
        lvlManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, 45 * Time.deltaTime * rotationSpeed);
    }

    // Use this for initialization
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player Player = other.GetComponent<player>();
            if(Player != null)
            {
                if (lvlManager.clear)
                {
                    Player.SavePlayerStats();
                    lvlManager.LevelCleared();
                }
                else
                {
                    dialogue.active = true;
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }

            }
            else
                Debug.Log("Character script not found on player");
        }
    }
}
