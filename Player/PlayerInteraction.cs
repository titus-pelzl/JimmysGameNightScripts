using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public class PlayerInteraction : MonoBehaviour
{
    public GameObject Target;
    public float MaxDistance;
    public string Typ;
    public bool Waiting;
    private NavMeshAgent agent;
    private GameObject DM;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        DM = GameObject.FindGameObjectWithTag("DialogueManager");
        DM.GetComponent<DialogueManager>().Player = gameObject;
    }
    private void Update()
    {
        if (Waiting)
        {
            if (Vector3.Distance(transform.position, Target.transform.position) < MaxDistance)
            {
                Waiting = false;
                agent.isStopped = true;
                agent.ResetPath();
                agent.velocity = Vector3.zero;
                LookAtTarget();
                CheckTyp();
            }
        }
    }

    public void Interrupt()
    {
        //benutzen wenn man während man wartet klickt
        //alles clearen, weil ich hab kein plan ob das notwendig ist, lass mich factorio spielen
        if (Waiting)
        {
            Waiting = false;
            Target = null;
            Typ = "";
        }
    }

    public void WalkToTarget()
    {
        //Welcome to ttttttarget
        agent.SetDestination(Target.transform.position);
    }

    public void LookAtTarget()
    {
        Vector3 LookTarget = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
        transform.LookAt(LookTarget);
    }

    public void DelayedLook(float time)
    {
        Invoke("LookAtTarget", time);
    }
    public void AnimationEventGrab()
    {
        Target.GetComponent<ItemInteractable>().PutItemInInventory();
    }

    public void CheckTyp()
    {
        switch (Typ)
        {
            case "Talk":
                DM.GetComponent<DialogueManager>().StartDialogue(Target.GetComponent<NPC>().FirstDialogue);
                break;
            case "Activate":
                Target.GetComponent<ItemActivateAble>().OnActivate();
                break;
            case "Take":
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("Ahngrof_Grab");
                //Target.GetComponent<ItemInteractable>().PutItemInInventory();   //In AnimationEventGrab() verschoben
                if (Target.GetComponent<ItemActivateAble>() != null)
                {
                    Typ = "Activate";
                    CheckTyp();
                }
                break;
            case "Combine":
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("Ahngrof_Give");
                
                GetComponent<PointNClick>().Inv.GetComponent<Inventory>().CombineTarget(Target);
                break;
        }
    }
}
