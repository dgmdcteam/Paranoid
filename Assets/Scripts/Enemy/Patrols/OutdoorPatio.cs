using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorPatio : MonoBehaviour
{
    [SerializeField] private Transform[] _referenceToMoveOutdoorPatio;
    [SerializeField] private GameObject[] _referencesOutdoorPatio;
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
        transform.position = Vector3.Lerp(transform.position, _referenceToMoveOutdoorPatio[1].position, 0.02f);
    }

    private void InitParameters()
    {
        _cont = 0;
        _referencesOutdoorPatio = new GameObject[5]; //Put exact number
        _referenceToMoveOutdoorPatio = new Transform[5]; //Put exact number

        #region Get references outdoor patio
        do
        {
            _referencesComplete = false;
            _referencesOutdoorPatio[_cont] = GameObject.Find("ReferenceOutdoorPatio" + _cont);
            _referenceToMoveOutdoorPatio[_cont] = _referencesOutdoorPatio[_cont].GetComponent<Transform>();
            _cont++;
            if (GameObject.Find("ReferenceOutdoorPatio" + _cont) == null)
            {
                _referencesComplete = true;
            }
        } while (_referencesComplete == false);
        #endregion
    }
}
