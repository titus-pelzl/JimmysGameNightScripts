using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable: MonoBehaviour
{
    //Dummy Script more or less
    public Item_SO item;
    public float MaxRange;

    public void PutItemInInventory()
    {
        //hmmmmmmm
        GameObject.FindGameObjectWithTag("PlayerData").GetComponent<Inventory>().AddItem(item);
        Destroy(gameObject);
    }
}
