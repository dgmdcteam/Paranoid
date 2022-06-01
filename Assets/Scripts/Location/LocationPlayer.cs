using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Detection._SharedInstance.ChangeEnabledPatrols(other.name);
    }
}
