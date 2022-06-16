using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPlayer : MonoBehaviour
{
    public GameObject noteT;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Location")
        {
            Detection._SharedInstance.ChangeEnabledPatrols(other.name);
            NotesOfLocation(other.name);
        }
    }

    public void NotesOfLocation(string name)
    {
        switch (name)
        {
            case "B":
                PlayerNotes._SharedInstance.NotesObtainedInLocationB();
                break;
            
            case "Patio":
                if (PlayerNotes._SharedInstance.nB == 0 && PlayerNotes._SharedInstance.nA == 0)
                {
                    PlayerNotes._SharedInstance.NotesObtainedInLocationPatio();
                    noteT.SetActive(true);
                }
                break;

            case "A":
                PlayerNotes._SharedInstance.NotesObtainedInLocationA();
                break;
        }
    }
}
