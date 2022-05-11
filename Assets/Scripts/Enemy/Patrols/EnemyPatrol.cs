using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _referenceToMoveA, _referenceToMoveB, _referenceToMoveC;
    [SerializeField] private GameObject[] _referencesA, _referencesB, _referencesC;
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
        transform.position = Vector3.Lerp(transform.position, _referenceToMoveB[1].position, 0.02f);
    }

    private void InitParameters()
    {
        _cont = 0;
        _referencesB = new GameObject[5]; //Put exact number
        _referenceToMoveB = new Transform[5]; //Put exact number
        /*
        #region Get references pavilion a
        do
        {
            _referencesComplete = false;
            _referencesA[_cont] = GameObject.Find("ReferenceA" + _cont);
            _referenceToMoveA[_cont] = _referencesB[_cont].GetComponent<Transform>();
            _cont++;
            if (GameObject.Find("ReferenceA" + _cont) == null)
            {
                _referencesComplete = true;
            }
        } while (_referencesComplete == false);
        #endregion
        */
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
        /*
        #region Get references pavilion c
        do
        {
            _referencesComplete = false;
            _referencesC[_cont] = GameObject.Find("ReferenceC" + _cont);
            _referenceToMoveC[_cont] = _referencesB[_cont].GetComponent<Transform>();
            _cont++;
            if (GameObject.Find("ReferenceC" + _cont) == null)
            {
                _referencesComplete = true;
            }
        } while (_referencesComplete == false);
        #endregion
        */
        GetComponent<Animator>().SetFloat("Speed", 2);

    }
}
