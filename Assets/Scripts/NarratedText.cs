using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarratedText : MonoBehaviour
{
    private string finalText = "";
    public string text = "";

    

    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] private float textDelay = 0.033f;
    [SerializeField] private float punctuationDelay = 0.3f;
    private float delay;

    [SerializeField] private int curLetterNum = 0;
    [SerializeField] private string curLetterString = " ";

    private float curDelay = 0.0f; //the delay to currently wait through
    public bool narrating = false;
    public bool allowSkip = true;
    public bool allowContinue = true;

    public int length = 0;

    public float updateClock = 0.0f;

    public AudioClip textSound;

    public string[] _ignoreChars = { "`", "~", "*", "(", ")", "-", "_", "=", "+", "{", "}", "[", "]", "'", "\\", "\n", "\t", "|", "<", ">", "/", "^", " ", "" };
    public string[] _punctuationChars = { ".", ",", "!", "?", ":", ";" };

    void Start()
    {
        //set text format and other variable stuff here
        delay = textDelay;

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
        AudioManager.PlayClip2D(textSound, 0.2f);
    }

    public void StartNarrating(float delayOverride = 0.0f)
    {
        if (delayOverride > 0.0f)
            delay = delayOverride;
        else
            delay = textDelay;

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
}
