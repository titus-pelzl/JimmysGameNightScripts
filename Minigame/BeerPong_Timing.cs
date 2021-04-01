using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerPong_Timing : MonoBehaviour
{
    public GameObject TimingCanvas;
    public GameObject ToleranceBox;
    public GameObject CounterSlider;

    [Range(0f,10f)]
    public float Difficulty;

    public float Counter = 10f;
    private float MinTolerance = 3f;
    private float MaxTolerance = 5f;
    private float TolerancePos = 0.5f;
    private float ToleranceSize = 0.2f;

    public float CounterSpeed;

    private bool CounterActive = false;
    private float CounterMul = 1f;
    private float BlinkTimer = 0f;

    private void Update()
    {
        if(BlinkTimer > 0f)
        {
            CounterSlider.GetComponent<Image>().color = Color.white;
        }
        else
        {
            CounterSlider.GetComponent<Image>().color = Color.red;
        }
        if (CounterActive)
        {
            if (Counter > 9.8f)
            {
                CounterMul = -1f;
            }
            if (Counter < 0.2f)
            {
                CounterMul = 1f;
            }

            Counter += CounterSpeed * CounterMul;

            CounterSlider.GetComponent<RectTransform>().anchorMin = new Vector2(0f, (Counter / 10) - 0.01f);
            CounterSlider.GetComponent<RectTransform>().anchorMax = new Vector2(1f, (Counter / 10) + 0.01f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Counter < MaxTolerance && Counter > MinTolerance)
            {
                //Success
                Debug.Log("Success");
                Success();
            }
            else
            {
                //failure
                BlinkTimer = 1f;
                GetComponent<BeerPong_Difficulty>().TimePunish();
            }
        }
        BlinkTimer -= 0.2f;
    }

    public void Activate()
    {
        TimingCanvas.SetActive(true);

        ToleranceSize = 2f * (1f-(Difficulty/10f)); //0 = 100%, 1 = 90% .... 5 = 50% 
        CounterSpeed = 0.12f + ((0.12f/10)*Difficulty);

        CounterActive = true;
        TolerancePos = Random.Range(2f,8f);


        MinTolerance = TolerancePos - (ToleranceSize / 2);
        MaxTolerance = TolerancePos + (ToleranceSize / 2);

        ToleranceBox.GetComponent<RectTransform>().anchorMin = new Vector2(0f, MinTolerance / 10);
        ToleranceBox.GetComponent<RectTransform>().anchorMax = new Vector2(1f, MaxTolerance / 10);

    }

    public void Success()
    {
        //Timer 3s hinzufügen?
        GetComponent<BeerPong_Difficulty>().TimeCounter += 3f;
        CounterActive = false;
        TimingCanvas.SetActive(false);
        GetComponent<BeerPong_Difficulty>().NextRound();
        enabled = false;
    }
}
