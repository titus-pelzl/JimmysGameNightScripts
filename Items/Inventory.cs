using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Sprite Inventory_Slot_Image;
    public GameObject Cursor_Item_Sprite;
    public List<Item_SO> Inventory_Content;

    public Color Transparent = new Color(1f, 1f, 1f, 0f);

    public Item_SO Held_Item;
    //public int Held_Item_index;
    public GameObject[] Buttons;

    public GameObject InventoryUI;
    public GameObject BagButton;

    //public void OpenInventory()
    //{
    //    InventoryUI.SetActive(true);
    //    DisplayItemsInInventory();
    //}
    //public void CloseInventory()
    //{
    //    AddItem(Held_Item);
    //    Held_Item = null;
    //    InventoryUI.SetActive(false);
    //}

    public void FoldInventoryOpen()
    {
        BagButton.GetComponent<Animator>().SetBool("Close", false);
        DisplayItemsInInventory();
    }
    public void FoldInventoryClosed()
    {
        BagButton.GetComponent<Animator>().SetBool("Close", true);
    }

    public void DisplayItemsInInventory() //zum refreshen der grafiken
    {
        for(int i=0;i<Inventory_Content.Count ; i++)
        {
            if(Inventory_Content[i] != null)
            {
                Buttons[i].transform.GetChild(0).GetComponent<Image>().sprite = Inventory_Content[i].Item_Sprite;
            }
        }
    }

    public void RemoveItem(int index)
    {
        Debug.Log("Removed item " + Inventory_Content[index].Item_Name + " from inventory");
        Inventory_Content[index] = null;
    }

    public void RemoveSpecificItem(Item_SO item)
    {
        for(int i = 0;i<Inventory_Content.Count ; i++)
        {
            if(Inventory_Content[i] == item)
            {
                Inventory_Content[i] = null;
                break;
            }
        }
    }

    public void AddItem(Item_SO item)
    {
        for (int i = 0; i < Inventory_Content.Count; i++)//hier werden bestimmt fehler auftreten
        {
            if(i == Inventory_Content.Count - 1)
            {
                Debug.Log("Inventory full");
                break;
            }
            if(Inventory_Content[i] == null)
            {
                Inventory_Content[i] = item;
                break;
            }
        }
    }
    public void PutInNextSlot(Item_SO item)
    {
        for (int i = 0; i < Inventory_Content.Count; i++)
        {
            if (Inventory_Content[i] != null)
            {
                InventoryDrop(i);
                break;
            }
        }
    }

    public void PutHandItemInNext()
    {
        PutInNextSlot(Held_Item);
    }

    public void InventoryButton(int index)
    {
        if(Held_Item != null)
        {
            InventoryDrop(index);
        }
        else
        {
            InventoryPickUp(index);
        }
    }

    public void InventoryPickUp(int index)
    {
        if (Inventory_Content[index] != null)
        {
            //Held_Item_index = index;
            Buttons[index].transform.GetChild(0).GetComponent<Image>().sprite = Inventory_Slot_Image;
            Held_Item = Inventory_Content[index];
            Cursor_Item_Sprite.GetComponent<Image>().sprite = Held_Item.Item_Sprite;
            Cursor_Item_Sprite.GetComponent<Image>().color = Color.white;
            Inventory_Content[index] = null;
        }
    }


    public void InventoryDrop(int index)
    {
        if(Inventory_Content[index] == null)//plazieren
        {
            Buttons[index].transform.GetChild(0).GetComponent<Image>().sprite = Held_Item.Item_Sprite;
            Inventory_Content[index] = Held_Item;
            ClearHand();

        }

        if (Inventory_Content[index] != null)//kombinieren
        {
            if (Held_Item != null)
            {
                Cursor_Item_Sprite.GetComponent<Image>().sprite = null;
                Cursor_Item_Sprite.GetComponent<Image>().color = Transparent;
                CombineItems(index);
            }
        }
    }

    public void CombineItems(int index)
    { 
        //ist item in hand kombinierbar
        if (Held_Item.Combineable != null)
        {
            //ist angeklicktes item kombinierbar
            if (Inventory_Content[index].Combineable != null)
            {
                //ist das die richtige kombi
                if (Held_Item.Combineable == Inventory_Content[index])
                {
                    Inventory_Content[index] = Held_Item.Combo_Result;
                    ClearHand();
                    DisplayItemsInInventory();
                }
                else
                {
                    Wrong();
                    Debug.Log("Falsche kombination");
                }
            }
            else
            {
                Wrong();
                Debug.Log("Nicht kombinierbar");
            }
        }
    }

    public void CombineTarget(GameObject Target)
    {
        for (int i = 0; i < Target.GetComponent<ItemActivateAble>().Combinations.Length; i++)
        {
            if (Target.GetComponent<ItemActivateAble>().Combinations[i].ValidComboItem == Held_Item)
            {
                Target.GetComponent<ItemActivateAble>().OnCombine(i);
                
            }
        }
        Wrong();
    }

    public void ClearImage()
    {

        Cursor_Item_Sprite.GetComponent<Image>().sprite = null;
        Cursor_Item_Sprite.GetComponent<Image>().color = Transparent;
    }

    public void ClearHand()
    {
        Held_Item = null;
        ClearImage();
    }

    public void Wrong()
    {
        //Wrong.mp3
    }
}
