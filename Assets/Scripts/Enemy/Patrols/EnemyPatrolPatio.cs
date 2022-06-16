using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolPatio : MonoBehaviour
{
    [SerializeField] private Transform[] _referenceToMovePatio;
    private int _cont, _situation, _randomNumber;
    private bool _referencesComplete;

    // Start is called before the first frame update
    void Start()
    {
        RandomDestinationEnemy();
    }

    public void RandomDestinationEnemy()
    {
        StopAllCoroutines();
        _randomNumber = (int)Random.Range(0, _referenceToMovePatio.Length);


        StartCoroutine(Patrol());


        GetComponent<Animator>().SetFloat("Speed", 1);
    }

    IEnumerator Patrol()
    {
        GetComponent<Animator>().SetFloat("Speed", 1);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _referenceToMovePatio[_randomNumber].transform.position, 0.05f);
            transform.LookAt(_referenceToMovePatio[_randomNumber].transform.position);
            if (Vector3.Distance(transform.position, _referenceToMovePatio[_randomNumber].transform.position) < 0.1f)
            {
                transform.position = _referenceToMovePatio[_randomNumber].transform.position;
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
