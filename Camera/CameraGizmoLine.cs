using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGizmoLine : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.GetChild(0).position,transform.GetChild(1).position);
    }
}
