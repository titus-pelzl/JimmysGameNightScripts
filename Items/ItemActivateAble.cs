using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemActivateAble : MonoBehaviour
{
    public string ObjectName;
    public float MaxRange;

    public bool[] Prerequisites;

    public UnityEvent Activate;

    public float ComboRange = 1.2f;
    public Combination[] Combinations;
    public void OnActivate()
    {
        Activate.Invoke();
    }

    public void OnCombine(int index)
    {
        if (Combinations[index].RequirepRerequisites)
        {
            if (Prerequisites[int.Parse(Combinations[index].Prerequisite)])
            {
                Combinations[index].OnCombine();
                GameObject.FindGameObjectWithTag("PlayerData").GetComponent<Inventory>().ClearHand();
            }
            //Wrong
            else
            {
                Debug.Log("Voraussetzung nicht erfüllt");
            }
        }
        else
        {
            Debug.Log("Benötigt keine Voraussetzung");
            Combinations[index].OnCombine();
            GameObject.FindGameObjectWithTag("PlayerData").GetComponent<Inventory>().ClearHand();
        }
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
}
