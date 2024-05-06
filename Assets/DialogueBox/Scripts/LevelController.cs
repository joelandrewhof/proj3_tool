using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] public DialogueController dialogueController;
    [SerializeField] public DialogueData dialogue1;
    [SerializeField] public DialogueData dialogue2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            dialogueController.DialogueStart(dialogue1);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            dialogueController.DialogueStart(dialogue2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            dialogueController.DialogueContinue();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dialogueController.DialogueSkip();
        }
    }
}
