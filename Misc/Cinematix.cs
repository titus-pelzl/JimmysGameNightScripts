using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cinematix : MonoBehaviour
{
    public GameObject BlackCurtain;

    public UnityEvent AfterDarkEvent;
    public UnityEvent EndingEvent;

    public Dialogue_Dialogue Intro;

    private bool BlendOn;
    private float BlendValue;
    private float BlendDirection = -0.008f;

    void Start()
    {
        GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(Intro);
    }

    public void AfterDark()
    {
        AfterDarkEvent.Invoke();
    }

    public void WillItBlend(bool state)
    {
        //yes it will blend make it dark pls
        if (state)
        {
            BlendValue = 0f;
            BlackCurtain.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, BlendValue);
            BlackCurtain.SetActive(true);
            BlendDirection = 0.008f;
            BlendOn = true;
        }

        //no pls dont blend give me light pls
        if (!state)
        {
            BlendDirection = -0.008f;
            BlendOn = true;
        }
    }

    private void Update()
    {
        if (BlendOn)
        {

            BlendValue += BlendDirection;
            BlackCurtain.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, BlendValue);
            //Wenn dunkel wird
            if (BlendDirection > 0f)
            {
                //und ende erreicht ist
                //gib ende
                if (BlendValue > 1f)
                {
                    BlendOn = false;
                    EndingEvent.Invoke(); //good bye
                }
            }

            //Wenn hell wird
            else if (BlendDirection < 0f)
            {
                //und ende erreicht ist
                if (BlendValue <= 0f)
                {
                    BlendOn = false;
                    BlackCurtain.SetActive(false);
                    //AfterDarkEvent.Invoke();
                }
            }
        }
    }

    public void BackToMenu(string Scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
    }

}
