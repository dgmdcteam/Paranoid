using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    private Transform _player;
    private Animator _animator;
    private bool _vision, _limitProximity, _attack;
    private float _radioVision, _radioProximity, _speed, _distance;
    string x;
    public GameObject attack;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        InitParameters();
        Debug.Log("Aqui follow");
        _attack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_attack == false)
        {
            Follow();
        }
        else
        {
            Attack();
        }

    }

    void Follow()
    {
        if (PlayerNotes._SharedInstance.allNotes == false)
        {
            if (Vector3.Distance(_player.position, transform.position) < _distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 0.07f);
                transform.LookAt(_player.transform);
                _animator.SetFloat("Speed", 2f);
                if (Vector3.Distance(_player.position, transform.position) < 1.5)
                {
                    _attack = true;
                }
            }
            else
            {
                //Detection._SharedInstance.ChangeEnabledPatrols(Detection._SharedInstance.locationBeforeFollow);
                GetComponent<EnemyFollowPlayer>().enabled = false;
            }
        }

    }

    void Attack()
    {
        if (Vector3.Distance(_player.position, transform.position) < 1.5)
        {
            attack.SetActive(true);
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 0f);
            _animator.SetBool("Attack", true);
        }
        else
        {
            attack.SetActive(false);
            _attack = false;
            _animator.SetBool("Attack", false);
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
