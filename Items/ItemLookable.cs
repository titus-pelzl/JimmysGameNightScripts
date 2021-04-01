using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLookable : MonoBehaviour
{
    public Dialogue_Dialogue InspectDialogue;

    public void ChangeLook(Dialogue_Dialogue dial)
    {
        InspectDialogue = dial;
    }
}
