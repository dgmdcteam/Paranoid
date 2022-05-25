using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "Patio":

                break;

            case "B":
                break;

            case "A":
                break;

            case "A1":
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
