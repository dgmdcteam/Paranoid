using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    private Transform _player;
    private Animator _animator;
    private bool _vision, _limitProximity;
    private float _radioVision, _radioProximity, _speed, _distance;
    string x;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        InitParameters();
        Debug.Log("Aqui follow");
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        if (Vector3.Distance(_player.position, transform.position) < _distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 0.1f);
            transform.LookAt(_player.transform);
            _animator.SetFloat("Speed", 2f);
        }
        else
        {
            Detection._SharedInstance.ChangeEnabledPatrols(Detection._SharedInstance.locationBeforeFollow);
            GetComponent<EnemyFollowPlayer>().enabled = false;
        }
    }

    private void InitParameters()
    {
        _radioVision = 2;
        _radioProximity = 1;
        _speed = 3;
        _distance = 7;
    }

    private void OnEnable()
    {
        Start();
    }
}
