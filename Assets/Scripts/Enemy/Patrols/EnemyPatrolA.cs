using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolA : MonoBehaviour
{
    [SerializeField] private Transform[] _referenceToMoveA;
    [SerializeField] private GameObject[] _referencesA;
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
        transform.position = Vector3.Lerp(transform.position, _referenceToMoveA[1].position, 0.02f);
    }

    private void InitParameters()
    {
        _cont = 0;
        _referencesA = new GameObject[5]; //Put exact number
        _referenceToMoveA = new Transform[5]; //Put exact number

        #region Get references pavilion a
        do
        {
            _referencesComplete = false;
            _referencesA[_cont] = GameObject.Find("ReferenceA" + _cont);
            _referenceToMoveA[_cont] = _referencesA[_cont].GetComponent<Transform>();
            _cont++;
            if (GameObject.Find("ReferenceA" + _cont) == null)
            {
                _referencesComplete = true;
            }
        } while (_referencesComplete == false);
        #endregion
    }
}
