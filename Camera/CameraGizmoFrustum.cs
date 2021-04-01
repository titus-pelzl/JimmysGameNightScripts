using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGizmoFrustum : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawFrustum(transform.position,transform.parent.GetComponent<Camera>().fieldOfView, transform.parent.GetComponent<Camera>().nearClipPlane, transform.parent.GetComponent<Camera>().farClipPlane, transform.parent.GetComponent<Camera>().aspect);
    }
}
