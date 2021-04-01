using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue",menuName = "Dialogues/Dialogue"),System.Serializable]
public class Dialogue_Dialogue : ScriptableObject
{
    [Tooltip("Name/ID des NPCs der Spricht")]
    public string Name;
    public Sentence[] Sentences;
    [Tooltip("Der Dialog der (/Die Auswahl die) nach diesem abgespielt wird")]
    public Object NextDialogue;

    [Tooltip("Bitte Bitte einen zahl ohne Space oder sonstiges hier reinschreiben sonst krieg ich gross aua")]
    public string Folge;
    //public Consequence Folge;
}