using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private bool enterZone = false;
    public GameObject textE;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enterZone && Input.GetKeyDown(KeyCode.E))
        {
            PlayerNotes._SharedInstance.NotesObtained();

            if (gameObject.tag == "NoteB")
            {
                --PlayerNotes._SharedInstance.nB;
            }

            if (gameObject.tag == "NoteA")
            {
                --PlayerNotes._SharedInstance.nA;
            }

            if (gameObject.tag == "NoteP")
            {
                --PlayerNotes._SharedInstance.nP;
            }

            textE.SetActive(false);
            Destroy(gameObject);
        }

        if (PlayerNotes._SharedInstance.notesObained == 10)
        {
            PlayerNotes._SharedInstance.allNotes = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enterZone = true;
            textE.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enterZone = false;
            textE.SetActive(false);
        }
    }
}
