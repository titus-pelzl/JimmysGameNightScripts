using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformLerp : MonoBehaviour
{
    //Script kommt auf die haupt kamera
    public Transform TargetTransform;

    public float TransitionSpeed;
    //private float Distance = 0f;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(GetComponent<CameraMoveSideways>().Zero_Position.position, GetComponent<CameraMoveSideways>().One_Position.position, GetComponent<CameraMoveSideways>().pointer), Time.deltaTime * TransitionSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation,TargetTransform.rotation, Time.deltaTime * TransitionSpeed);
        gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(gameObject.GetComponent<Camera>().fieldOfView, GetComponent<CameraMoveSideways>().Zero_Position.transform.parent.GetComponent<Camera>().fieldOfView, Time.deltaTime * TransitionSpeed);
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position,TargetTransform.position) < 0.1f)
        {
            GetComponent<CameraMoveSideways>().Activated = true;
            GetComponent<CameraTransformLerp>().enabled = false;


        }
    }
}
