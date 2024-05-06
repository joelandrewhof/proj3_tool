using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData_", menuName = "UnitData/Dialogue")]
public class DialogueData : ScriptableObject
{
    [SerializeField][Tooltip("This affects what icon sprite and text sound will be used.")] 
    public string character;
    [SerializeField] [Tooltip("The sequence of dialogue to be displayed.")]
    public string[] dialogue;
}
