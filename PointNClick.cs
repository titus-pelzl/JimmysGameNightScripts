using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointNClick : MonoBehaviour
{
    [HideInInspector]
    public Camera cam;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public bool Paused = false;
    private GameObject DM;
    public float LerpFactor = 0.5f;
    private Vector3 LastMove = Vector3.zero;

    private GameObject IC; //itemcursor
    public GameObject Inv;
    
    private void Start()
    {
        IC = GameObject.FindGameObjectWithTag("Cursor");
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        DM = GameObject.FindGameObjectWithTag("DialogueManager");
        Inv = GameObject.FindGameObjectWithTag("PlayerData");
        DM.GetComponent<DialogueManager>().Player = this.gameObject;
    }


    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            //Links
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!Paused)
                {
                    //Item in Hand?
                    if (Inv.GetComponent<Inventory>().Held_Item != null)
                    {
                        if (IsMouseOverUIWithIgnores())
                        {
                            //Nichts
                        }
                        else
                        {
                            if (hit.transform.gameObject.GetComponent<ItemActivateAble>() != null)
                            {
                                //hat ziel eine kombination?
                                if (hit.transform.gameObject.GetComponent<ItemActivateAble>().Combinations.Length != 0)
                                {
                                    //Inv.GetComponent<Inventory>().CombineTarget(hit.transform.gameObject);
                                    Combine(hit.transform.gameObject);
                                }
                                else
                                {
                                    Inv.GetComponent<Inventory>().PutHandItemInNext();
                                }
                            }
                        }
                    }
                    //UI?           

                    else
                    {
                        if (!IsMouseOverUIWithIgnores())
                        {
                            //N: Reguläre interaktion wie bereits gescriptet

                                if (!(DM.GetComponent<DialogueManager>().Talking))
                                {
                                    if (!(DM.GetComponent<DialogueManager>().WaitingForChoice))
                                    {
                                        if (hit.transform.tag == "NPC")
                                        {
                                            //Sprechen
                                            Talk(hit.transform.gameObject);
                                        }
                                        else
                                        {
                                            if (hit.transform.gameObject.GetComponent<ItemActivateAble>() != null)
                                            {
                                                //Aktivieren, dann nehmen wenn geht
                                                Activate(hit.transform.gameObject);
                                                if (hit.transform.gameObject.GetComponent<ItemInteractable>() != null)
                                                {
                                                    Take(hit.transform.gameObject);
                                                }
                                            }
                                            else if (hit.transform.gameObject.GetComponent<ItemInteractable>() != null)
                                            {
                                                //nehmen
                                                Take(hit.transform.gameObject);
                                            }
                                            else
                                            {
                                                //laufen
                                                Walk(hit.point);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //Wenn am Sprechen, überspringen
                                    Skip();
                                }

                        }
                        //UI? Yes
                        else
                        {
                            //Y: Lass den Button sich auslösen falls er da ist
                        }
                    }
                }
            }
            //Rechts
            if (Input.GetKeyDown(KeyCode.Mouse1) && !IsMouseOverUIWithIgnores())
            {
                if (!Paused)
                {
                    if (!(DM.GetComponent<DialogueManager>().Talking))
                    {
                        if (!(DM.GetComponent<DialogueManager>().WaitingForChoice))
                        {
                            if(hit.transform.gameObject.GetComponent<ItemLookable>() != null)
                            {
                                //angucken
                                Look(hit.transform.gameObject);
                            }
                        }
                    }
                }
            }
        }

        if(GetComponent<NavMeshAgent>().velocity.magnitude > 0.2f)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Ahngrof_Walking", true);
            RotateToSpeed();
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Ahngrof_Walking", false);
        }

    }

    public void Skip()
    {
        //iterate
        DM.GetComponent<DialogueManager>().VoiceBox.Stop();
        DM.GetComponent<DialogueManager>().IterateDialogue();
    }

    public void Look(GameObject GO)
    {
        //Stoppen
        agent.isStopped = true;
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        //ItemLookable
        DM.GetComponent<DialogueManager>().Talking = true;
        DM.GetComponent<DialogueManager>().StartDialogue(GO.GetComponent<ItemLookable>().InspectDialogue);
        //Zum ziel gucken
        GetComponent<PlayerInteraction>().Target = GO;
        GetComponent<PlayerInteraction>().DelayedLook(0.05f);
        //GetComponent<PlayerInteraction>().LookAtTarget();
    }
    public void Walk(Vector3 pos)
    {
        GetComponent<PlayerInteraction>().Interrupt();
        agent.isStopped = false;
        agent.SetDestination(pos);
    }

    public void Activate(GameObject GO)
    {
        GetComponent<PlayerInteraction>().Target = GO;
        GetComponent<PlayerInteraction>().MaxDistance = GO.GetComponent<ItemActivateAble>().MaxRange;
        GetComponent<PlayerInteraction>().Typ = "Activate";
        GetComponent<PlayerInteraction>().WalkToTarget();
        GetComponent<PlayerInteraction>().Waiting = true;

    }
    public void Talk(GameObject GO)
    {
        DM.GetComponent<DialogueManager>().Talking = true;
        GetComponent<PlayerInteraction>().Target = GO;
        GetComponent<PlayerInteraction>().MaxDistance = GO.GetComponent<NPC>().MaxInteractionDistance;
        GetComponent<PlayerInteraction>().Typ = "Talk";
        GetComponent<PlayerInteraction>().WalkToTarget();
        GetComponent<PlayerInteraction>().Waiting = true;
    }
    public void Take(GameObject GO)
    { 
        GetComponent<PlayerInteraction>().Target = GO;
        GetComponent<PlayerInteraction>().MaxDistance = GO.GetComponent<ItemInteractable>().MaxRange;
        GetComponent<PlayerInteraction>().Typ = "Take";
        GetComponent<PlayerInteraction>().WalkToTarget();
        GetComponent<PlayerInteraction>().Waiting = true;
    }

    public void Combine(GameObject GO)
    {
        GetComponent<PlayerInteraction>().Target = GO;
        GetComponent<PlayerInteraction>().MaxDistance = GO.GetComponent<ItemActivateAble>().ComboRange;
        GetComponent<PlayerInteraction>().Typ = "Combine";
        GetComponent<PlayerInteraction>().WalkToTarget();
        GetComponent<PlayerInteraction>().Waiting = true;
    }

    public void RotateToSpeed()
    {
        Vector3 NormDirection = transform.position + Vector3.Normalize(GetComponent<NavMeshAgent>().steeringTarget - transform.position);
        Vector3 LerpResult = Vector3.Lerp(LastMove,NormDirection,LerpFactor);
        transform.LookAt(LerpResult);
        LastMove = LerpResult;
    }


    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    private bool IsMouseOverUIWithIgnores()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<MouseUIIgnore>() != null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }
        return raycastResultList.Count > 0;
    }
}