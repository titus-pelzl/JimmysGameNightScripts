using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{

    public GameObject PauseMenuUI;

    public void SetUnactive()
    {
        PauseMenuUI.SetActive(false);
    }
    public void TimeScale()
    {
        Time.timeScale = 0f;
    }
}
