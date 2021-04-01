using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ahngrof_Animation_Events : MonoBehaviour
{
    public UnityEvent OnGrab;

    public void GrabEvent()
    {
        GameObject.Find("Plyr(Clone)").GetComponent<PlayerInteraction>().AnimationEventGrab();
        OnGrab.Invoke();
    }
}
