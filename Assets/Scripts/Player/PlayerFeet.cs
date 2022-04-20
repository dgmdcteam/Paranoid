using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    private PlayerMove _playerMove;
    private bool _floor;

    public bool Floor
    {
        get => _floor;
        set => _floor = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        Floor = true;
    }

    private void OnTriggerStay(Collider other)
    {
        _playerMove.CanJump = true;
        //playerMove.Anim.SetBool("TouchFloor", true);
        Floor = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerMove.CanJump = false;
        //playerMove.Anim.SetBool("TouchFloor", false);
        Floor = false;
    }
}
