using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private uint _sensibility, _fieldOfView;

    public uint Sensibility
    {
        get => _sensibility;
        set => _sensibility = value;
    }

    public uint FieldOfView
    {
        get => _fieldOfView;
        set => _fieldOfView = value;
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
