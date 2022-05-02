using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _referenceToMoveB;
    [SerializeField] private GameObject[] _referencesB;
    private int _cont;
    private bool _referencesComplete;

    // Start is called before the first frame update
    void Start()
    {
        InitParameters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitParameters()
    {
        _cont = 0;
        _referencesB = new GameObject[5]; //Put exact number
        _referenceToMoveB = new Transform[5]; //Put exact number
        #region Get references pavilion b
        do
        {
            _referencesComplete = false;
            _referencesB[_cont] = GameObject.Find("ReferenceB" + _cont);
            _referenceToMoveB[_cont] = _referencesB[_cont].GetComponent<Transform>();
            _cont++;
            if (GameObject.Find("ReferenceB" + _cont) == null)
            {
                _referencesComplete = true;
            }
        } while (_referencesComplete == false);
        #endregion
        GetComponent<NavMeshAgent>().SetDestination(_referenceToMoveB[4].position);
        GetComponent<NavMeshAgent>().speed = 3;

    }
}
