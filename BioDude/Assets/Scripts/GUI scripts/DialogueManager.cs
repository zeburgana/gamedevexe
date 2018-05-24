using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    private Text DialogueText;
    private Text NameText;
    private Image Avatar;
    private GameObject DialogueCanvas;
    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<Sprite> avatars;
    player _player;


    private Animator animator;

    float time;


    // Use this for initialization
    void Start () {
        _player = GameObject.Find("player").GetComponent<player>();
        DialogueCanvas = GameObject.Find("Dialogue canvas");
        DialogueText = DialogueCanvas.transform.Find("DialogueText").GetComponent<Text>();
        NameText = DialogueCanvas.transform.Find("NameText").GetComponent<Text>();
        Avatar = DialogueCanvas.transform.Find("Avatar").GetComponent<Image>();
        animator = DialogueCanvas.transform.Find("DialoguePanel").GetComponent<Animator>();

        sentences = new Queue<string>();
        names = new Queue<string>();
        avatars = new Queue<Sprite>();
        time = Time.timeScale;
        animator.SetBool("IsOpen", false);
        DialogueCanvas.SetActive(true);
    }

    public void StartDialogue(DialogueColliderTrigger DialogueData)
    {
        if (DialogueData.active)
        {
            time = Time.timeScale;
            Time.timeScale = 0;
            _player.SetAbleToMove(false);

            animator.SetBool("IsOpen", true);
            sentences.Clear();
            DialogueData.active = false;

            foreach (Dialogue DialogueUnit in DialogueData.dialogue)
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
        _player.SetAbleToMove(true);
    }
    public void SetDialogueState(bool state)
    {
        ;
    }
    public bool IsDialogueOpen()
    {
        return false;
    }
}