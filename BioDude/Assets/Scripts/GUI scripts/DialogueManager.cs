using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public Text DialogueText;
    public Text NameText;
    private Queue<string> sentences;
    private Queue<string> names;

    public Animator animator;

    float time;


    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
        names = new Queue<string>();
        time = Time.timeScale;
        animator.SetBool("IsOpen", false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (!animator.GetBool("WasOpen"))
        {
            Time.timeScale = 0;


            animator.SetBool("IsOpen", true);
            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            foreach (string name in dialogue.names)
            {
                names.Enqueue(name);
            }
            DisplayNextSentence();
        }
    }
    public void DisplayNextSentence()
    {
        Debug.Log("dialogue");
        if(sentences.Count == 0)
        {
            EndDialogue();
        }
        NameText.text = names.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }
    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        animator.SetBool("WasOpen", true);
        Time.timeScale = time;
        Debug.Log("End of conversation");
    }
}