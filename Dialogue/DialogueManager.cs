using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject TextBox;//Canvas
    public GameObject TextDisplay;//Text

    public GameObject ChoiceBackground;
    public GameObject[] ChoiceButtons;

    private int Sentence_Index;
    private List<Sentence> NPC_sentences = new List<Sentence>();
    private Object Next_Dial;
    public GameObject Current_NPC;
    private string Folge;
    [HideInInspector]
    public AudioSource VoiceBox;

    //[HideInInspector]
    public GameObject Player;

    //[HideInInspector]
    public bool Talking;
    [HideInInspector]
    public bool WaitingForChoice;

    public List<Choice> TempChoices = new List<Choice>();

    private void Start()
    {
        VoiceBox = GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue_Dialogue dialogue)
    {
        Talking = true;
        //alle Sätze speichern
        NPC_sentences.Clear();
        NPC_sentences = dialogue.Sentences.ToList<Sentence>();

        //NPC Speichern
        GameObject[] AllNPCs = GameObject.FindGameObjectsWithTag("NPC");
        foreach(GameObject npc in AllNPCs)
        {
            if(npc.GetComponent<NPC>().Name == dialogue.Name)
            {
                Current_NPC = npc;
                break;
            }
        }

        //folge speichern
        Folge = dialogue.Folge;

        //NextDial speichern
        Next_Dial = null;
        if (dialogue.NextDialogue != null)
        {
            Next_Dial = dialogue.NextDialogue;
        }

        //Sätze durch iterieren
        Sentence_Index = 0;
        IterateDialogue();

    }
    
    public void IterateDialogue()
    {
        CancelInvoke();
        if(Sentence_Index >= NPC_sentences.Count)
        {
            //Konsequenz ausführen
            Execute();

            //Ist nächster dialog vorhanden?
            if(Next_Dial != null)
            {
                CheckType(Next_Dial);
            }
            else
            {
                //vollstop
                StopDialogue();
            }
        }   
        else
        {
            //Nächste Iteration
            SentenceInBox(NPC_sentences[Sentence_Index]);
            if (NPC_sentences[Sentence_Index].VoiceLine != null)
            {
                //Debug.Log("Wait "+ NPC_sentences[Sentence_Index].VoiceLine.length + "s");
                Invoke("IterateDialogue", NPC_sentences[Sentence_Index].VoiceLine.length);
            }
            else
            {
                //Debug.Log("Wait 2s'");
                Invoke("IterateDialogue", 2f);
            }
            Sentence_Index++;
        }
    }

    public void StopDialogue()
    {
        Talking = false;
        Player.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
        Player.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        //Dialogfenster schließen
        TextDisplay.GetComponent<TextMeshProUGUI>().text = "";
        TextBox.SetActive(false);
    }

    public void Execute()
    {
        if (Folge != "")
        {
            Current_NPC.GetComponent<NPC>().Consequences[int.Parse(Folge)].Invoke();
        }
        Folge = "";
    }

    public void SentenceInBox(Sentence sent) //Rake in Lake
    {
        TextBox.transform.position = Camera.main.WorldToScreenPoint(DialogueTextPosition.GetTopPoint(Current_NPC));

        VoiceBox.GetComponent<AudioSource>().Stop();
        TextDisplay.GetComponent<TextMeshProUGUI>().text = "";
        if(sent.VoiceLine != null)
        {
            VoiceBox.GetComponent<AudioSource>().PlayOneShot(sent.VoiceLine);
        }
        TextDisplay.GetComponent<TextMeshProUGUI>().text = sent.Text;
        TextBox.SetActive(true);
    }

    public void CheckType(Object obj)
    {
        if(obj.GetType() == typeof(Dialogue_Dialogue))
        {
            StartDialogue((Dialogue_Dialogue)obj);
        }
        if (obj.GetType() == typeof(Dialogue_Choice))
        {
            SummonTheButtons((Dialogue_Choice)obj);
        }
    }

    public void SummonTheButtons(Dialogue_Choice cho)//Level 2 Evocation Spell, Dex-Saving throw, damage is halved on successful saving-throw, 3d2 damage
    {
        ChoiceBackground.SetActive(true);
        TempChoices.Clear();
        for (int i = 0; i < cho.Choices.Length; i++)
        {

            if (cho.Choices[i].Requires_Prerequisite)
            {
                //Ist Choice an "i" verfügbar
                if (Current_NPC.GetComponent<NPC>().Prerequisites[cho.Choices[i].Prerequisite])//Wenn bedingung erfüllt ist
                {//füge zu TempChoices hinzu
                    TempChoices.Add(cho.Choices[i]);
                }
            }
            else
            {
                TempChoices.Add(cho.Choices[i]);
            }
        }

        for (int i = 0; i < TempChoices.Count; i++)
        {
            //Buttons aktivieren an stelle "i"
            ChoiceButtons[i].SetActive(true);
            //Ändere Button inhalt an stell "i" mit inhalt aus Tempchoices an stelle "i"
            ChoiceButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = TempChoices[i].Description;
        }
    }

    public void ChoiceButton(int index)
    {
        ChoiceBackground.SetActive(false);
        for(int i = 0;i<ChoiceButtons.Length ; i++)
        {
            ChoiceButtons[i].SetActive(false);
        }
        StartDialogue((Dialogue_Dialogue)TempChoices[index].NextDialogue);
    }
}