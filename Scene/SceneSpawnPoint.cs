using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnPoint : MonoBehaviour
{
    public List<Transform> SpawnPoints;

    public GameObject Sword;

    public void Start()
    {
        foreach (Transform Child in transform)
        {
            SpawnPoints.Add(Child);

        }

        GameObject Player = Resources.Load<GameObject>("Prefabs/Plyr");
        GameObject ply =Instantiate(Player,SpawnPoints[SceneSwitchInfo.NextSpawnPoint].position,new Quaternion(0f,0f,0f,0f));
        Sword = ply.transform.GetChild(0).GetChild(1).gameObject;
        Cursor.SetCursor(Resources.Load<Texture2D>("Cursor/cursor.idle"),Input.mousePosition,CursorMode.ForceSoftware);
    }
}
