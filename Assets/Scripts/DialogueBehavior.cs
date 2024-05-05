using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBehavior : MonoBehaviour
{
    [SerializeField] private Color textColor = Color.white;
    [SerializeField] private Color boxColor = Color.black;

    [SerializeField] private DialogueData dialogue;
    [SerializeField] private Image icon;
    private NarratedText narratedText;

    public bool dialogueCompleted = false; //deactivation flag

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

        if(currentLine < dialogue.text.Length)
        {
            narratedText.SetText(dialogue.text[currentLine]);
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
        return narratedText.narrating;
    }

    public bool CanContinue()
    {
        return narratedText.allowContinue;
    }
    public bool CanSkip()
    {
        return narratedText.allowSkip;
    }
}
