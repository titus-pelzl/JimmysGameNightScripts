using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item"), System.Serializable]
public class Item_SO : ScriptableObject
{
    public string Item_Name;
    public Sprite Item_Sprite;

    public Item_SO Combineable;
    public Item_SO Combo_Result;
}
