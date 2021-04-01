using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateInScene : MonoBehaviour
{

    public Transform LeftRot;
    public Transform RightRot;
    private Vector3 BetwVect;
    private Vector3 ABetwVect;
    public float pointer = 0.5f;
    public float RotateSpeed = 0.01f;
    public float MaxRoateTime= 0.1f;
    public float MaxRoateSpeed = 0.3f;
    [Tooltip("Bereich ab dem die Kameraschwenkt"),Range(0f,0.5f)]
    public float EdgeTriggerWidth = 0.15f;
    private Vector3 SaveMe;

    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 PlyScrPos = Camera.main.WorldToViewportPoint(Player.transform.position);
        if ((PlyScrPos.x) > (1-EdgeTriggerWidth))//RECHTS!!!
        {
            pointer += RotateSpeed;
        }
        if ((PlyScrPos.x) < (0+EdgeTriggerWidth))//LINKS!!!!
        {
            pointer -= RotateSpeed;
        }
        pointer = Mathf.Clamp(pointer,0f,1f);

        BetwVect = Vector3.Lerp(LeftRot.position,RightRot.position,pointer);
        ABetwVect = Vector3.SmoothDamp(SaveMe, BetwVect, ref ABetwVect, MaxRoateTime,MaxRoateSpeed);
        transform.rotation = Quaternion.LookRotation(Vector3.SmoothDamp(SaveMe, BetwVect, ref ABetwVect, Time.deltaTime, MaxRoateSpeed) - transform.position, Vector3.up);

        Debug.DrawLine(transform.position, BetwVect, Color.red,Time.deltaTime);
        Debug.DrawLine(transform.position, ABetwVect, Color.blue, Time.deltaTime);
        SaveMe = Vector3.Lerp(LeftRot.position, RightRot.position, pointer);
    }
}
