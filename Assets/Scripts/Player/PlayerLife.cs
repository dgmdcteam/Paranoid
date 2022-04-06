using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private uint _life, _maxLife;

    public uint Life
    {
        get => _life;
        set => _life = value;
    }

    public uint MaxLife
    {
        get => _maxLife;
        set => _maxLife = value;
    }
    private void Awake()
    {
        InitParameters();
    }

    private void InitParameters()
    {
        MaxLife = 2;
        Life = MaxLife;
    }
}
