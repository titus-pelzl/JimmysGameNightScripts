using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    public bool Requires_Prerequisite;
    public int Prerequisite;
    public string Description;
    public ScriptableObject NextDialogue;
}
