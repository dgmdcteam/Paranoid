using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNotes : MonoBehaviour
{
    public static PlayerNotes _SharedInstance;
    public TextMeshProUGUI textNotesObtained, textB, textA, textPatio, textGeneral;
    public int notesObained, nB, nA, nP;
    public bool allNotes = false;

    private void Awake()
    {
        if (_SharedInstance != null)
        {
            return;
        }

        _SharedInstance = this;
    }

    private void Start()
    {
        nB = 4;
        nA = 5;
        nP = 1;
    }

    public void NotesObtained()
    {
        notesObained += 1;
        textNotesObtained.text = notesObained.ToString();
    }

    public void NotesObtainedInLocationB()
    {
        if (nB != 0)
        {
            textB.enabled = true;
            textGeneral.enabled = true;
            textB.text = nB.ToString();
            textGeneral.text = "notas restantes en B";
            StartCoroutine(DesactivateText());
        }
    }

    public void NotesObtainedInLocationA()
    {
        if (nA != 0)
        {
            textA.enabled = true;
            textGeneral.enabled = true;
            textA.text = nA.ToString();
            textGeneral.text = "notas restantes en A";
            StartCoroutine(DesactivateText());
        }
    }

    public void NotesObtainedInLocationPatio()
    {
        textPatio.enabled = true;
        textGeneral.enabled = true;
        textPatio.text = nP.ToString();
        textGeneral.text = "notas restantes en Patio";
        StartCoroutine(DesactivateText());
    }

    IEnumerator DesactivateText()
    {
        yield return new WaitForSeconds(2);
        textGeneral.enabled = textPatio.enabled = textA.enabled = textB.enabled = false;
    }
}
