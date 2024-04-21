using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBehavior : MonoBehaviour
{
    [SerializeField] private Color textColor = Color.white;
    [SerializeField] private Color boxColor = Color.black;

    [SerializeField] private DialogueData dialogue;
    private NarratedText narratedText;

    private bool dialogueCompleted = false;

    private int currentLine = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        narratedText = gameObject.GetComponent<NarratedText>();
    }

    public void NextLine()
    {
        if (!narratedText.allowContinue)
            return;

        narratedText.text = "";

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
        if(narratedText.allowSkip)
        {
            narratedText.SkipNarration();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
