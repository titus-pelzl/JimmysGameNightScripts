using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeerPong_Difficulty : MonoBehaviour
{    
    public int PartsWon;
    public GameObject Timer;
    public float TimeCounter = -0.1f;
    public bool CounterActive;

    private void Update()
    {
        if (CounterActive)
        {
            Timer.GetComponent<TextMeshProUGUI>().text = TimeCounter.ToString();
            TimeCounter -= Time.deltaTime;
            if (TimeCounter < 0f)
            {
                //You Lose, Good day sir!
                Failure();
            }
        }
    }
    public void TimePunish()
    {
        TimeCounter -= 2f;
    }

    public void StartGame()
    {
        if (TimeCounter<0f)
        {
            Debug.Log("timer start");
            //Falls jemand es schafft die runde abzuschließen wenn die Zeit unter 0 ist, wird der timer auf 10 gesetzt ohne negative konsequenzen
            TimeCounter = 10f;
        }
        Debug.Log("neue runde");
        CounterActive = true;
        GetComponent<BeerPong_Aiming>().enabled = true;
        GetComponent<BeerPong_Aiming>().Activate();
        GetComponent<BeerPong_Aiming>().ShuffleArrows(PartsWon+4);

        GetComponent<BeerPong_Aiming>().Crosshair.transform.position += GetComponent<BeerPong_Aiming>().AllArrowDisplacement(transform.position);
    }

    public void NextRound()
    {
        if(PartsWon>=3)
        {
            //final win
            Victory();
        }
        else
        {
            PartsWon++;
            GetComponent<BeerPong_Timing>().Difficulty = PartsWon;
            StartGame();
        }
    }

    public void Failure()
    {
        //alle werte reseten, npc triggern, kamera ändern
        //Alle Skripte ausmachen
        Debug.Log("failure");
        CounterActive = false;
        GetComponent<BeerPong_Timing>().TimingCanvas.SetActive(false);
        GetComponent<BeerPong_Timing>().enabled = false;
        GetComponent<BeerPong_Aiming>().AimingCanvas.SetActive(false);
        GetComponent<BeerPong_Aiming>().enabled = false;
    }

    public void Victory()
    {
        //das gleiche wie beim verlieren, bloß triggert es etwas anderes
        Debug.Log("Final Win");
        CounterActive = false;
        GetComponent<BeerPong_Timing>().TimingCanvas.SetActive(false);
        GetComponent<BeerPong_Timing>().enabled = false;
        GetComponent<BeerPong_Aiming>().AimingCanvas.SetActive(false);
        GetComponent<BeerPong_Aiming>().enabled = false;
    }
}