using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChairClimben : MonoBehaviour
{
    public GameObject DM;
    private GameObject Player;

    public bool OnChair = false;

    public Transform TP1;
    public Transform TP2;
    void Start()
    {
        DM = GameObject.FindGameObjectWithTag("DialogueManager");
    }

    public void Climb()
    {
        Player = DM.GetComponent<DialogueManager>().Player;
        if (OnChair)
        {
            OnChair = false;
            Player.GetComponent<NavMeshAgent>().Warp(TP2.position);
        }
        else
        {
            OnChair = true;
            Player.GetComponent<NavMeshAgent>().Warp(TP1.position);
        }
    }
}
