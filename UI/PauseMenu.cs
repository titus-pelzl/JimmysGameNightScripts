using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject PauseButton;
    public GameObject Gerd;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PlayerPaused(false);
        PauseButton.SetActive(true);
        //PauseMenuUI.SetActive(false);
        PauseMenuUI.GetComponent<Animator>().SetBool("Pause", false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Gerd.SetActive(true);
    }

    

    public void Pause()
    {
        
        PauseButton.SetActive(false);
        PauseMenuUI.SetActive(true);
        PauseMenuUI.GetComponent<Animator>().SetBool("Pause", true);
        PlayerPaused(true);
        //Time.timeScale = 0f;
        GameIsPaused = true;
        Gerd.SetActive(false);
    }


    public void MainMenu()
    {
        PlayerPaused(false);

        Time.timeScale = 1f;
        GameIsPaused = false;

        SceneManager.LoadScene(0);
    }

    public void PlayerPaused(bool state)
    {
        GameObject[] GOs;
        GOs = GameObject.FindGameObjectsWithTag("NPC");
        foreach(GameObject go in GOs)
        {
            if(go.GetComponent<NPC>().Name == "Ahngrof")
            {
                go.GetComponent<PointNClick>().Paused = state;
            }
        }
    }

}