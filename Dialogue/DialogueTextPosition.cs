using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueTextPosition
{
    public static Vector3 GetTopPoint(GameObject GO)
    {
        Mesh mesh = GO.GetComponent<MeshFilter>().mesh;
        Vector3 toppoint = GO.transform.position + new Vector3(0f,mesh.bounds.extents.y * GO.transform.localScale.y,0f);

        return(toppoint);
    }
}
