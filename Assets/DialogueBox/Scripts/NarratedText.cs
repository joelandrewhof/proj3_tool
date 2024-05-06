using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarratedText : MonoBehaviour
{
    [Header("Text Variables")]
    private string finalText = "";
    public string text = "";

    [Header("Objects")]
    [SerializeField] private TextMeshProUGUI textMesh;

    [Header("Audio")]
    public AudioClip textSound;
    [SerializeField] public float volume = 0.2f;

    [Header("Text Delays")]
    [SerializeField][Tooltip("The amount of timea regular letter is paused on.")]
    private float defaultDelay = 0.033f;
    [SerializeField][Tooltip("The delay for punctuation characters.")] 
    private float punctuationDelay = 0.3f;
    private float delay; //the current delay length
    private float curDelay = 0.0f; //a timer for the delay to wait through

    //letter tracker variables
    private int curLetterNum = 0;
    private string curLetterString = " ";
    private int length = 0;

    //tracking the state of narration
    private bool narrating = false;
    private bool allowSkip = true;
    private bool allowContinue = true;

    //tracking update loop
    private float updateClock = 0.0f;

    //lists of special characters that affect delay time
    [Header("Special Characters")]
    public string[] _ignoreChars = { "`", "~", "*", "(", ")", "-", "_", "=", "+", "{", "}", "[", "]", "'", "\\", "\n", "\t", "|", "<", ">", "/", "^", " ", "" };
    public string[] _punctuationChars = { ".", ",", "!", "?", ":", ";" };

    void Start()
    {
        //set text format and other variable stuff here
        delay = defaultDelay;

        narrating = false;
        allowContinue = true;

        SetSound();

        //textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }


    public void SetText(string text = "")
    {
        finalText = text;
        length = finalText.Length;
    }

    public void SetSound(string character = "sample")
    {
        textSound = Resources.Load<AudioClip>
            ("Sounds/Dialogue/" + character);
    }

    public void PlaySound()
    {
        AudioManager.PlayClip2D(textSound, volume);
    }

    public void StartNarrating(float delayOverride = 0.0f)
    {
        if (delayOverride > 0.0f)
            delay = delayOverride;
        else
            delay = defaultDelay;

        narrating = true;
    }

    public string AddCurrentLetter()
    {
        string nextLetter = finalText.Substring(curLetterNum, 1);
        text += nextLetter;
        curLetterString = nextLetter;

        //only play sound if this is a regular letter
        if (System.Array.IndexOf(_ignoreChars, nextLetter) == -1 &&
            System.Array.IndexOf(_punctuationChars, nextLetter) == -1)
        {
            PlaySound();
        }


        if (curLetterNum >= length - 1)
        {
            curLetterNum = 0;
            FinishNarration();
        }
        else
            curLetterNum++;

        return nextLetter;
    }

    public string SetNextDelayFromLetter(string letter)
    {
        if (System.Array.IndexOf(_ignoreChars, letter) != -1)
        {
            curDelay = delay;
            return "IGNORE";
        }
        else if (System.Array.IndexOf(_punctuationChars, letter) != -1)
        {
            curDelay = punctuationDelay;
            return "PUNCTUATION";
        }
        else
        {
            curDelay = delay;
            return "NORMAL";
        }
    }

    public void FinishNarration()
    {
        narrating = false;
        text = finalText;
        allowContinue = true;
    }

    public void SkipNarration()
    {
        curLetterNum = 0;
        FinishNarration();
    }

    void Update()
    {
        if(narrating)
        {
            updateClock += Time.deltaTime;

            if(updateClock >= curDelay)
            {
                string addedLetter = AddCurrentLetter();
                SetNextDelayFromLetter(addedLetter);
                PlaySound();
                updateClock = 0.0f;
            }
        }

        textMesh.text = text;
    }
    public bool IsNarrating()
    {
        return narrating;
    }

    public bool CanContinue()
    {
        return allowContinue;
    }
    public bool CanSkip()
    {
        return allowSkip;
    }
}
