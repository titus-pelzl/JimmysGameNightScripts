using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject PrevCamera;
    public GameObject NextCamera;

    public void Switch()
    {
        GetComponent<CameraMoveSideways>().enabled = false;
        GetComponent<CameraTransformLerp>().enabled = true;
    }

    private void Start()
    {
        
    }

    void LateUpdate()
    {
        
    }
}
