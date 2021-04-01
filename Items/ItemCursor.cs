using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCursor : MonoBehaviour
{
    private bool ClearText = true;
    private GameObject Inv;
    private Inventory Inv_Comp;

    private void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("PlayerData");
        Inv_Comp = Inv.GetComponent<Inventory>();
    }
    void Update()
    {
        ActivateHand(false);
        ActivateEye(false);

        if (ClearText)
        {
            ClearDisplay();
            ClearText = false;
        }
        transform.position = Input.mousePosition;

        ////////////////////

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<ItemInteractable>() != null)
            {
                ActivateHand(true);
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hit.transform.gameObject.GetComponent<ItemInteractable>().item.Item_Name;
                ClearText = true;
            }
            if (hit.transform.gameObject.GetComponent<ItemLookable>() != null)
            {
                ActivateEye(true);
            }

            if (hit.transform.gameObject.GetComponent<ItemActivateAble>() != null)
            {
                ActivateHand(true);
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hit.transform.gameObject.GetComponent<ItemActivateAble>().ObjectName;
                ClearText = true;

                //if (Input.GetMouseButtonDown(0))
                //{
                //    if (Inv_Comp.Held_Item != null)
                //    {
                //        if (Inv_Comp.Held_Item == hit.transform.gameObject.GetComponent<ItemActivateAble>().ValidComboItem)
                //        {
                //            Inv.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("Ahngrof_Grab");
                //            Inv_Comp.Held_Item = null;
                //            ClearText = true;
                //            Inv_Comp.ClearImage();
                //            hit.transform.gameObject.GetComponent<ItemActivateAble>().OnCombine();
                //        }
                //        else
                //        {
                //            Debug.Log("Wrong Combination");
                //        }
                //    }
                //    else
                //    {
                //        hit.transform.gameObject.GetComponent<ItemActivateAble>().OnActivate();
                //    }
                //}
            }
        }
        //////////////////////////
    }



    

    public void Wrong()
    {
        //play sound oder so, "Der DM sagt nein"
        Debug.Log("Wrong Combo");
    }
    
    public void DisplayItemOnButton(int index)
    {
        List<Item_SO> items = Inv_Comp.Inventory_Content;

        if (items[index]!=null)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = items[index].Item_Name;
        }
    }
      
    public void ClearDisplay()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
    }

    public void ActivateEye(bool state)
    {
        transform.parent.GetChild(1).gameObject.GetComponent<Animator>().SetBool("Open",state);
    }
    public void ActivateHand(bool state)
    {
        transform.parent.GetChild(0).gameObject.GetComponent<Animator>().SetBool("Open", state);
    }

}
