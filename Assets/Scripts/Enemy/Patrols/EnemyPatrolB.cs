using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolB : MonoBehaviour
{
    [SerializeField] private Transform[] _referenceToMoveB, _referenceToMoveStairB;
    private int _cont, _situation, _randomNumber, _beforeB;
    private bool _referencesComplete, _stairsComplete;

    // Start is called before the first frame update
    void Start()
    {
        _beforeB = 0;
        RandomDestinationEnemy();
    }

    public void RandomDestinationEnemy()
    {
        StopAllCoroutines();
        _randomNumber = (int)Random.Range(0, _referenceToMoveB.Length);

        if ((_beforeB > 7 && _randomNumber > 7) || (_beforeB < 8 && _randomNumber < 8))
        {
            _situation = 0;
        }
        else if (_beforeB < 8 && _randomNumber > 7)
        {
            _situation = 1;
        }
        else if (_beforeB > 7 && _randomNumber < 8)
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


        GetComponent<Animator>().SetFloat("Speed", 1);
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

        _stairsComplete = false;

        while (true)
        {

            switch (x)
            {
                case 0:
                    if (Vector3.Distance(transform.position, _referenceToMoveStairB[0].transform.position) < 1.5f)
                    {
                        if (_situation == 1)
                        {
                            x++;
                        }
                        else
                        {
                            _stairsComplete = true;
                            StopCoroutine(LessOrMoreHeightDestination());
                            StartCoroutine(Patrol());
                            break;
                        }
                        GetComponent<Animator>().SetFloat("Speed", 1);
                    }
                    break;

                case 1:
                    if (Vector3.Distance(transform.position, _referenceToMoveStairB[1].transform.position) < 1.5f)
                    {
                        if (_situation == 1)
                        {
                            x++;
                        }
                        else
                        {
                            x--;
                        }
                        GetComponent<Animator>().SetFloat("Speed", 1);
                    }
                    break;

                case 2:
                    if (Vector3.Distance(transform.position, _referenceToMoveStairB[2].transform.position) < 1.5f)
                    {
                        if (_situation == 1)
                        {
                            _stairsComplete = true;
                            StopCoroutine(LessOrMoreHeightDestination());
                            StartCoroutine(Patrol());
                            break;
                        }
                        else
                        {
                            x--;
                        }
                        GetComponent<Animator>().SetFloat("Speed", 1);

                    }
                    break;
            }

            if (_stairsComplete == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _referenceToMoveStairB[x].transform.position, 0.05f);
                transform.LookAt(_referenceToMoveStairB[x].position);
            }

            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator Patrol()
    {
        GetComponent<Animator>().SetFloat("Speed", 1);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _referenceToMoveB[_randomNumber].transform.position, 0.05f);
            transform.LookAt(_referenceToMoveB[_randomNumber].transform.position);
            if (Vector3.Distance(transform.position, _referenceToMoveB[_randomNumber].transform.position) < 0.1f)
            {
                transform.position = _referenceToMoveB[_randomNumber].transform.position;
                GetComponent<Animator>().SetFloat("Speed", 0);
                StartCoroutine(LookAroundArea(transform));
                StopCoroutine(Patrol());
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator LookAroundArea(Transform self)
    {
        Quaternion start = self.rotation;
        Quaternion left = start * Quaternion.Euler(0, -180, 0);
        Quaternion right = start * Quaternion.Euler(0, 180, 0);

        yield return Rotate(self, start, left, 1f);
        yield return new WaitForSeconds(0.5f);

        yield return Rotate(self, left, right, 2f);
        yield return new WaitForSeconds(0.5f);

        yield return Rotate(self, right, start, 1f);
        yield return new WaitForSeconds(0.5f);

        if (Random.Range(0, 2) == 0)
        {
            yield return Rotate(self, start, left, 1f);
            yield return new WaitForSeconds(0.5f);

            yield return Rotate(self, left, right, 2f);
            yield return new WaitForSeconds(0.5f);

            yield return Rotate(self, right, start, 1f);
            yield return new WaitForSeconds(1);
        }
        
        RandomDestinationEnemy();

    }

    IEnumerator Rotate(Transform self, Quaternion from, Quaternion to, float duration)
    {

        for (float t = 0; t < 1f; t += Time.deltaTime / duration)
        {
            self.rotation = Quaternion.Slerp(from, to, t);
            yield return null;
        }

        yield return new WaitForSeconds(5);
        self.rotation = to;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        Start();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
