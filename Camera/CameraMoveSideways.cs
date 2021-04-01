using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveSideways : MonoBehaviour
{
    [Range(0f,1f)]
    public float pointer;
    public Transform Zero_Position;
    public Transform One_Position;
    public float DistOne;
    public float DistZero;
    private GameObject Player;

    public GameObject PlayerProjection;
    public Vector3 ProjectionPoint;
    public float ProjToOne;
    public float ProjToZero;

    public bool Activated = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        ProjectionPoint = Vector3.Project(Player.transform.position - Zero_Position.position, (One_Position.position - Zero_Position.position)) + Zero_Position.position;

        ProjToOne = Vector3.Distance(One_Position.position,ProjectionPoint)/ Vector3.Distance(One_Position.position,Zero_Position.position);
        ProjToZero = Vector3.Distance(Zero_Position.position , ProjectionPoint) / Vector3.Distance(One_Position.position, Zero_Position.position);

        pointer = Vector3.Magnitude(ProjectionPoint-Zero_Position.position) / Vector3.Magnitude(One_Position.position - Zero_Position.position);

        if(ProjToZero>=1f&& ProjToZero > ProjToOne)
        {
            pointer = 1f;
        }
        if(ProjToOne>=1f&& ProjToOne > ProjToZero)
        {
            pointer = 0f;
        }

        Debug.DrawLine(Zero_Position.position, ProjectionPoint, Color.red, Time.deltaTime);
        Debug.DrawLine(Zero_Position.position, One_Position.position, new Color(0f,1f,0f,0.2f), Time.deltaTime);
        //Debug.DrawLine(Zero_Position.position, Zero_Position.position + Vector3.Normalize(Zero_Position.position - One_Position.position), Color.blue, Time.deltaTime);
        PlayerProjection.transform.position = ProjectionPoint;
    }
    private void LateUpdate()
    {
        if(Activated)
        {
            gameObject.GetComponent<Camera>().fieldOfView = Zero_Position.transform.parent.GetComponent<Camera>().fieldOfView;
            transform.position = Vector3.Lerp(Zero_Position.position, One_Position.position, pointer);
            transform.rotation = Quaternion.Lerp(Zero_Position.rotation, One_Position.rotation, pointer);

        }
    }

    
}
