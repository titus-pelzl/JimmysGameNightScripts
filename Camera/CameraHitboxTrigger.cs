using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHitboxTrigger : MonoBehaviour
{
    public Camera ActivatedCamera;
    private Transform _tf;
    public void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "LeftTrigger")
        {
            Camera.main.gameObject.GetComponent<CameraMoveSideways>().pointer = 1;
        }
        if (gameObject.name == "RightTrigger")
        {
            Camera.main.gameObject.GetComponent<CameraMoveSideways>().pointer = 0;
        }

        Camera.main.gameObject.GetComponent<CameraMoveSideways>().Activated = false;
        Camera.main.gameObject.GetComponent<CameraTransformLerp>().enabled = true;

        
        if (gameObject.name == "LeftTrigger")
        {
            Camera.main.gameObject.GetComponent<CameraTransformLerp>().TargetTransform = ActivatedCamera.transform.GetChild(1).transform;
        }
        if (gameObject.name == "RightTrigger")
        {
            Camera.main.gameObject.GetComponent<CameraTransformLerp>().TargetTransform = ActivatedCamera.transform.GetChild(0).transform;
        }

        Camera.main.gameObject.GetComponent<CameraMoveSideways>().Zero_Position = ActivatedCamera.transform.GetChild(0).transform;
        Camera.main.gameObject.GetComponent<CameraMoveSideways>().One_Position = ActivatedCamera.transform.GetChild(1).transform;

        //Meine Trigger aus
        transform.parent.GetChild(2).gameObject.SetActive(false);
        transform.parent.GetChild(3).gameObject.SetActive(false);
        //Andere Trigger an
        ActivatedCamera.transform.GetChild(2).gameObject.SetActive(true);
        ActivatedCamera.transform.GetChild(3).gameObject.SetActive(true);

        
    }

}
