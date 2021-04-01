using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFocus : MonoBehaviour
{
    public GameObject FocusedObject;

    private Vector2 OffsetValues;
    
    public Vector3 PlayerToFocus;
    private Vector2 CameraHorOffset;//X = X-Offset, Y = Z-Offset
    private Camera ControlledCamera;
    private void Start()
    {
        ControlledCamera = Camera.main;
    }

    public void Update()
    {
        OffsetValues = new Vector2(Vector3.Magnitude    (
                                                                new Vector3 (transform.position.x, transform.GetChild(0).gameObject.transform.position.y, transform.position.z) -
                                                                            transform.GetChild(0).gameObject.transform.position),
                                                        transform.GetChild(0).gameObject.transform.position.y - transform.position.y
                                                        );

        PlayerToFocus = transform.position - FocusedObject.transform.position;

        CameraHorOffset = new Vector2 ( OffsetValues.x * (new Vector2(Vector3.Normalize(PlayerToFocus).x,Vector3.Normalize(PlayerToFocus).z).x),
                                        OffsetValues.x * (new Vector2(Vector3.Normalize(PlayerToFocus).x,Vector3.Normalize(PlayerToFocus).z).y));

        ControlledCamera.transform.position =   transform.position +
                                                new Vector3(CameraHorOffset.x,OffsetValues.y,CameraHorOffset.y);
        ControlledCamera.transform.rotation = Quaternion.LookRotation(FocusedObject.transform.position-ControlledCamera.transform.position,Vector3.up);
    }
}
