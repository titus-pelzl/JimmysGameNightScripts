using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchTrigger : MonoBehaviour
{
    //Auf Hitbox ziehen
    public string NextScene;
    public int NextSpawnPoint;
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Wechsel in " + NextScene + ", an SpawnPoint " + NextSpawnPoint);
            SceneSwitchInfo.NextSpawnPoint = NextSpawnPoint;
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextScene);
        }
    }

}
