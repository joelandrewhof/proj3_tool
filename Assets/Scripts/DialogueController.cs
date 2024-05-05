using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] public DialogueBehavior diaBehavior;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DialogueStart(DialogueData dialogue = null)
    {
        diaBehavior.gameObject.SetActive(true);
        diaBehavior.SetDialogue(dialogue);
        DialogueContinue();
    }

    public void DialogueContinue(bool force = false)
    {
        if(force || (diaBehavior.CanContinue() && !diaBehavior.IsNarrating()))
        {
            diaBehavior.NextLine();
        }
        else
        {
            Debug.Log("Could not continue dialogue");
        }
    }

    public void DialogueSkip(bool force = false)
    {
        if(force || diaBehavior.CanSkip())
        {
            diaBehavior.SkipNarration();
        }
        else
        {
            Debug.Log("Could not skip dialogue");
        }
    }
}
