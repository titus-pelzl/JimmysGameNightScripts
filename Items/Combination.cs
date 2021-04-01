using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Combination
{
    public Item_SO ValidComboItem;
    public bool RequirepRerequisites = false;
    public string Prerequisite;
    public UnityEvent Combine;
    public void OnCombine()
    {
        Combine.Invoke();
    }
}