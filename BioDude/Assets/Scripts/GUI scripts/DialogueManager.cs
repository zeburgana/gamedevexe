using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public Text DialogueText;
    public Text NameText;
    public Image Avatar;
    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<Sprite> avatars;
    private bool DialogueOpen = false;

    public Animator animator;

    float time;


    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
        names = new Queue<string>();
        avatars = new Queue<Sprite>();
        time = Time.timeScale;
        animator.SetBool("IsOpen", false);
    }

    public void StartDialogue(Dialogue[] DialogueData)
    {
        if (!animator.GetBool("WasOpen"))
        {
            time = Time.timeScale;
            Time.timeScale = 0;


            animator.SetBool("IsOpen", true);
            DialogueOpen = true;
            sentences.Clear();

            foreach (Dialogue DialogueUnit in DialogueData)
            {
                sentences.Enqueue(DialogueUnit.sentence);
                names.Enqueue(DialogueUnit.name);
                avatars.Enqueue(DialogueUnit.avatar);
            }

            DisplayNextSentence();
        }
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        NameText.text = names.Dequeue();
        Avatar.sprite = avatars.Dequeue();

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
        DialogueOpen = false;
    }
    public void SetDialogueState(bool state)
    {
        DialogueOpen = state;
    }
    public bool IsDialogueOpen()
    {
        return DialogueOpen;
    }
}