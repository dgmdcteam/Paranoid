using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _vision, _limitProximity;
    private float _radioVision, _radioProximity, _speed, _distance;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        InitParameters();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_player.position, transform.position) < _distance)
        {
            _agent.SetDestination(_player.position);
            _agent.speed = _speed;
            _animator.SetFloat("Speed", 0.2f);
        }
        else
        {
            _agent.speed = _speed * 0;
            _animator.SetFloat("Speed", 0);
        }
    }

    private void InitParameters()
    {
        _radioVision = 2;
        _radioProximity = 1;
        _speed = 3;
        _distance = 10;
    }
}
