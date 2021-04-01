using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Choice")]
public class Dialogue_Choice : ScriptableObject
{
    public Choice[] Choices;
}
