using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueColliderTrigger : MonoBehaviour {

    public Dialogue[] dialogue;


    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("enter");
        if(collision.gameObject.tag=="Player")
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
