using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    [Tooltip("Name/ID des NPCs, darf nicht den gleichen Namen wie andere NPCs in der Szene haben")]
    public string Name;
    private GameObject DM;
    public float MaxInteractionDistance;
    public Dialogue_Dialogue FirstDialogue;

    public bool[] Prerequisites;

    public UnityEvent[] Consequences;

    void Start()
    {
        DM = GameObject.FindGameObjectWithTag("DialogueManager");
    }
    public void TogglePrerequisite(int index)
    {
        if (Prerequisites[index])
        {
            Prerequisites[index] = false;
        }
        else
        {
            Prerequisites[index] = true;
        }
    }

    public void ChangeDialogue(Dialogue_Dialogue Dialogue)
    {
        FirstDialogue = Dialogue;
    }
}
