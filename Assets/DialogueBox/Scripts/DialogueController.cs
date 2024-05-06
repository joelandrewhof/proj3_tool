using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] public DialogueBehavior dialogueBehavior;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DialogueStart(DialogueData dialogue = null)
    {
        dialogueBehavior.gameObject.SetActive(true);
        dialogueBehavior.SetDialogue(dialogue);
        DialogueContinue();
    }

    public void DialogueContinue(bool force = false)
    {
        if(force || (dialogueBehavior.CanContinue() && !dialogueBehavior.IsNarrating()))
        {
            dialogueBehavior.NextLine();
        }
        else
        {
            Debug.Log("Could not continue dialogue");
        }
    }

    public void DialogueSkip(bool force = false)
    {
        if(force || dialogueBehavior.CanSkip())
        {
            dialogueBehavior.SkipNarration();
        }
        else
        {
            Debug.Log("Could not skip dialogue");
        }
    }
}
