using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneric : MonoBehaviour
{
    [Tooltip("Soll die Interaktion mit diesem Item Reichweiten mäßig limitert werden")]
    public bool LimitInteractionRange;
    [Tooltip("Wenn Ja wie weit soll die Distanz in Unity-Einheiten sein (ka was das ist)")]
    public float MaxInteractionDistance;

    [TextArea(3,5)]
    public string TestText;
    public float TextTime = 1f;
}
