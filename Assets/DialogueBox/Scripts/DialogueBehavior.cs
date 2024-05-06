using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBehavior : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] private Image icon;
    private DialogueData dialogue;
    private NarratedText narratedText;

    private bool dialogueCompleted = false; //deactivation flag

    private int currentLine = -1;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        narratedText = gameObject.GetComponent<NarratedText>();
    }

    public void SetDialogue(DialogueData newDialogue = null)
    {
        if(newDialogue != null)
        {
            dialogue = newDialogue;
        }
        currentLine = -1;
        dialogueCompleted = false;

        SetIconSprite();
        SetSound();
    }

    public void SetIconSprite()
    {
        icon.sprite = Resources.Load<Sprite>
            ("Sprites/DialoguePortraits/" + dialogue.character);
    }

    public void SetSound()
    {
        narratedText.SetSound(dialogue.character);
    }

    public void NextLine()
    {
        if (!CanContinue())
            return;

        narratedText.text = "";
        currentLine++;

        if(currentLine < dialogue.dialogue.Length)
        {
            narratedText.SetText(dialogue.dialogue[currentLine]);
            narratedText.StartNarrating();
        }
        else
        {
            dialogueCompleted = true;
            gameObject.SetActive(false);
        }
    }

    public void SkipNarration()
    {
        if(CanSkip())
        {
            narratedText.SkipNarration();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //some getters for the controller class
    public bool IsNarrating()
    {
        return narratedText.IsNarrating();
    }

    public bool CanContinue()
    {
        return narratedText.CanContinue();
    }
    public bool CanSkip()
    {
        return narratedText.CanSkip();
    }
}
