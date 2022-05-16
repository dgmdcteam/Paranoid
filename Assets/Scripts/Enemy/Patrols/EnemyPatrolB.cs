using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolB : MonoBehaviour
{
    [SerializeField] private Transform[] _referenceToMoveB, _referenceToMoveStairB;
    [SerializeField] private GameObject[] _referencesB, _referencesStairsB;
    private int _cont, _situation, _randomNumber, _beforeB;
    private bool _referencesComplete;

    // Start is called before the first frame update
    void Start()
    {
        //InitParameters();
        _beforeB = 0;
        RandomDestinationEnemy();
    }

    public void RandomDestinationEnemy()
    {
        StopAllCoroutines();
        _randomNumber = (int)Random.Range(0, _referenceToMoveB.Length);
        Debug.Log("Mov edificio B " + _referenceToMoveB[_randomNumber].transform.name);
        Debug.Log("Random en random dest " + _randomNumber + " Before " + _beforeB);

        if ((_beforeB > 1 && _randomNumber > 1) || (_beforeB < 2 && _randomNumber < 2))
        {
            _situation = 0;
        }
        else if (_beforeB < 2 && _randomNumber > 1)
        {
            _situation = 1;
        }
        else if (_beforeB > 1 && _randomNumber < 2)
        {
            _situation = 2;
        }
        _beforeB = _randomNumber;


        if (_situation == 0)
        {
            StartCoroutine(Patrol());
        }
        else
        {
            StartCoroutine(LessOrMoreHeightDestination());
        }
        

        GetComponent<Animator>().SetFloat("Speed", 2);
    }

    IEnumerator LessOrMoreHeightDestination()
    {
        int x;

        if (_situation == 1)
        {
            x = 0;
        }
        else
        {
            x = 2;
        }

        while (true)
        {
            //Debug.Log("Mov escaleras " +  _referenceToMoveStairB[x].transform.name + " X " + x);

            switch (x)
            {
                case 0:
                    if (Vector3.Distance(transform.position, _referenceToMoveStairB[0].transform.position) < 1.5f)
                    {
                        GetComponent<Animator>().SetFloat("Speed", 0);
                        if (_situation == 1)
                        {
                            x++;
                        }
                        else
                        {
                            StopCoroutine(LessOrMoreHeightDestination());
                            StartCoroutine(Patrol());
                            break;
                        }
                        GetComponent<Animator>().SetFloat("Speed", 2);
                    }
                    break;

                case 1:
                    if (Vector3.Distance(transform.position, _referenceToMoveStairB[1].transform.position) < 1.5f)
                    {
                        GetComponent<Animator>().SetFloat("Speed", 0);
                        if (_situation == 1)
                        {
                            x++;
                        }
                        else
                        {
                            x--;
                        }
                        GetComponent<Animator>().SetFloat("Speed", 2);
                    }
                    break;

                case 2:
                    if (Vector3.Distance(transform.position, _referenceToMoveStairB[2].transform.position) < 1.5f)
                    {
                        GetComponent<Animator>().SetFloat("Speed", 0);
                        if (_situation == 1)
                        {
                            StopCoroutine(LessOrMoreHeightDestination());
                            StartCoroutine(Patrol());
                            break;
                        }
                        else
                        {
                            x--;
                        }
                        GetComponent<Animator>().SetFloat("Speed", 2);
                    }
                    break;
            }
            transform.position = Vector3.MoveTowards(transform.position, _referenceToMoveStairB[x].transform.position, 0.05f);
            transform.LookAt(_referencesStairsB[x].transform.position);

            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator Patrol()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _referenceToMoveB[_randomNumber].transform.position, 0.05f);
            transform.LookAt(_referenceToMoveB[_randomNumber].transform.position);
            if (Vector3.Distance(transform.position, _referenceToMoveB[_randomNumber].transform.position) < 1.5f)
            {
                Debug.Log("Aqui 0");
                StopCoroutine(Patrol());
                RandomDestinationEnemy();
                break;
            }
            //GetComponent<Rigidbody>().WakeUp();
            //Debug.Log("Patrol " + _referenceToMoveB[_randomNumber].transform.name);
            yield return new WaitForEndOfFrame();
        }
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
    }
}
