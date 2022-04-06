using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    private uint _stamina, _maxStamina;

    public uint Stamina
    {
        get => _stamina;
        set => _stamina = value;
    }

    public uint MaxStamina
    {
        get => _maxStamina;
        set => _maxStamina = value;
    }

    private void Awake()
    {
        InitParameters();
    }

    private void InitParameters()
    {
        MaxStamina = 10;
        Stamina = MaxStamina;
    }
}
