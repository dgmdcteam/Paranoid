using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Example : MonoBehaviour
{
    public Transform target;
    private NavMeshPath path;
    private float elapsed = 0.0f;
    [SerializeField] private Transform[] _referenceToMoveA, _referenceToMoveB, _referenceToMoveC;
    [SerializeField] private GameObject[] _referencesA, _referencesB, _referencesC;
    private int _cont;
    private bool _referencesComplete;
    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
        InitParameters();
    }

    void Update()
    {
        // Update the way to the goal every second.
        elapsed += Time.deltaTime;
        
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(transform.position, _referenceToMoveB[1].position, NavMesh.AllAreas, path);
            GetComponent<NavMeshAgent>().SetDestination(_referenceToMoveB[1].position);
            //transform.position = Vector3.Lerp(transform.position, _referenceToMoveB[1].position, 0.02f);
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }

    private void InitParameters()
    {
        _cont = 0;
        _referencesB = new GameObject[5]; //Put exact number
        _referenceToMoveB = new Transform[5]; //Put exact number
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
    }
}
