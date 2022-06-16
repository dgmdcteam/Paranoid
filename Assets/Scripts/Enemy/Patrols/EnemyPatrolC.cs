using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolC : MonoBehaviour
{
    [SerializeField] private Transform[] _referenceToMoveC;
    [SerializeField] private GameObject[] _referencesC;
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
        transform.position = Vector3.Lerp(transform.position, _referenceToMoveC[1].position, 0.02f);
    }

    private void InitParameters()
    {
        _cont = 0;
        _referencesC = new GameObject[5]; //Put exact number
        _referenceToMoveC = new Transform[5]; //Put exact number

        #region Get references pavilion c
        do
        {
            _referencesComplete = false;
            _referencesC[_cont] = GameObject.Find("ReferenceC" + _cont);
            _referenceToMoveC[_cont] = _referencesC[_cont].GetComponent<Transform>();
            _cont++;
            if (GameObject.Find("ReferenceC" + _cont) == null)
            {
                _referencesComplete = true;
            }
        } while (_referencesComplete == false);
        #endregion
    }
}
