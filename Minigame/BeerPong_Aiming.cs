using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerPong_Aiming : MonoBehaviour
{
    public GameObject AimingCanvas;
    public List<int> Arrows = new List<int> { }; //0 = up, 1 = right, 2 down, 3 left
    private int CurrentArrow;
    public float DisplacementAmount = 2f;

    public GameObject Crosshair;

    public Sprite Arrow;
    public GameObject ArrowFrame;

    private float ArrowPulseValue = 1f;
    public float ArrowPulseSpeed = 0.02f;

    public void DisplayArrowImage(int dir)
    {
        ArrowFrame.GetComponent<Image>().sprite = Arrow;
        ArrowFrame.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f * dir));
    }

    public void ShuffleArrows(int num)
    {
        Arrows = new List<int> { };
        CurrentArrow = 0;

        for (int i = 0; i < num; i++)
        {
            Arrows.Add(Random.Range(0, 3)); 
        }
    }

    public void Warp()//testing 
    {
        ShuffleArrows(4);
        Crosshair.transform.position += AllArrowDisplacement(transform.position);
    }

    public void MoveCrosshair()
    {
        Crosshair.transform.position -= ArrowDisplacement(Arrows[CurrentArrow],CurrentArrow);
    }

    public Vector3 ArrowDisplacement(int dir, int index)
    {
        Vector3 v3 = Vector3.up; //ein sehr schiefer motor
        v3 = Quaternion.AngleAxis(90 * Arrows[index], Vector3.back) * v3;
        //CurrentArrow++;
        return (v3 * (DisplacementAmount * Mathf.Pow(index, 0.5f)));
    }

    public Vector3 AllArrowDisplacement(Vector3 StartPos)//crosshair am anfang hiermit verschieben
    { 
        Vector3 TempDisplacement = Vector3.zero;

        for(int i = 0 ;i<Arrows.Count ;i++)
        {
            TempDisplacement += ArrowDisplacement(Arrows[i], i);
        }
        CurrentArrow = 0;
        DisplayArrowImage(Arrows[CurrentArrow]);
        return TempDisplacement;
    }

    public void InputDirection(int dir)//WASD input entspricht jeweils einer zahl W 0, D 1, S 2, A 3
    {
        ArrowPulseValue = 1.3f;
        if (dir == Arrows[CurrentArrow])
        {
            CurrentArrow++;
            if (CurrentArrow == Arrows.Count)
            {
                //Win win, nächster minigame teil, timing game
                Debug.Log("Next");
                Success();
            }
            else
            {
                DisplayArrowImage(Arrows[CurrentArrow]);
                MoveCrosshair();
            }
        }
        else
        {
            Debug.Log("fail");
            GetComponent<BeerPong_Difficulty>().TimePunish();
            CurrentArrow = 0;
            ArrowFrame.GetComponent<Image>().color = Color.red;
            ShuffleArrows(4);
            AllArrowDisplacement(new Vector3(0f, -5.2f, 10f));
        }
    }
    public void Activate()
    {
        AimingCanvas.SetActive(true);
    }

    public void Success()
    {
        AimingCanvas.SetActive(false);
        GetComponent<BeerPong_Timing>().enabled = true;
        GetComponent<BeerPong_Timing>().Activate();
        enabled = false;
    }

    private void Update()
    {
        if (ArrowPulseValue > 1.05f)
        {
            ArrowPulseValue -= ArrowPulseSpeed;
        }
        else
        {
            ArrowPulseValue = 1f;
            ArrowFrame.GetComponent<Image>().color = Color.white;
        }

        ArrowFrame.GetComponent<RectTransform>().localScale = Vector3.one * ArrowPulseValue;

        if (Input.GetKeyDown(KeyCode.W))
        {
            InputDirection(0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            InputDirection(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            InputDirection(2);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            InputDirection(3);
        }
    }

}
