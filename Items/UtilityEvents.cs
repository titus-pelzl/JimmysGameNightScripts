using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UtilityEvents : MonoBehaviour
{
    public UnityEvent GotCandles;
    public int Candles = 0;
    public GameObject[] CandleGO;
    public UnityEvent BenghisEvent;

    public GameObject Mug;
    public GameObject MugParent;

    public GameObject PlayerBarer;

    public bool BeerHad = false;
    public void AddCandle()
    {
        CandleGO[Candles].SetActive(true);
        Candles++;
        if(Candles >= 3)
        {
            GotCandles.Invoke();
        }
    }

    public void RespawnMug()
    {
        if (MugParent.transform.childCount == 0)
        {
            GameObject mog = Instantiate(Mug, MugParent.transform);
        }
        MugParent.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public int Drunkness = 0;
    public UnityEvent[] DrunkEvents;

    public void DoDrunkThings()
    {
        DrunkEvents[Drunkness].Invoke();
    }

    public void AddDrunky()
    {
        Drunkness++;
    }

    public void GiveSwordBack()
    {
        PlayerBarer.GetComponent<SceneSpawnPoint>().Sword.SetActive(true);
    }

    public float BlackValue = 0f;
    public float BlackSpeed;
    public bool BlackOn;
    public GameObject BlackCurtain;
    public void MiniGame(bool state) //schwarzblende :|
    {
        BlackCurtain.SetActive(state);
        BlackOn = state;
        if (state)
        {
            BlackSpeed = 0.008f;
        }
    }

    public void HadBeer()
    {
        if (!BeerHad)
        {
            BeerHad = true;
            BenghisEvent.Invoke();
        }
    }

    public UnityEvent Minigame;

    private void Update()
    {
        if (BlackOn)
        {
            BlackValue += BlackSpeed;
            BlackCurtain.GetComponent<Image>().color = new Color(0f, 0f, 0f, BlackValue);
            if (BlackSpeed > 0f)
            {
                if (BlackValue > 1f)
                {
                    Minigame.Invoke();
                    BlackSpeed = -0.008f;
                }
            }
            else if (BlackSpeed < 0f)
            {
                if (BlackValue <= 0f)
                {
                    BlackOn = false;
                    BlackCurtain.SetActive(false);
                    MiniGame(false);
                }
            }
        }
    }
}
