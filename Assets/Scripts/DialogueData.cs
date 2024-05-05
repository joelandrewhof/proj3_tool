using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData_", menuName = "UnitData/Dialogue")]
public class DialogueData : ScriptableObject
{
    [SerializeField] public string character;
    //[SerializeField] public int fontSize = 48;
    [SerializeField] public string[] text;
}
