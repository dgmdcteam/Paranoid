using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public static Detection _SharedInstance;

    public enum actualLocation
    {
        Patio,
        A,
        A1,
        B
    }

    public actualLocation _actualLocation;

    [SerializeField] private float _distance, _angle;
    [SerializeField] private LayerMask _targetLayers, _obstacleLayers;
    [SerializeField] private Collider _detectedTarget;
    [SerializeField] private Collider[] _colliders;
    public string locationBeforeFollow;

    private void Awake()
    {
        if (_SharedInstance != null)
        {
            return;
        }

        _SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerNotes._SharedInstance.allNotes == false)
        {
            _colliders = Physics.OverlapSphere(transform.position, // Enemigo
               _distance, // Distancia de comprobacion
               _targetLayers); // Player y Base
            _detectedTarget = null;

            foreach (Collider collider in _colliders)
            {
                Vector3 directionToCollider = collider.bounds.center - transform.position; // No normalizado
                directionToCollider = Vector3.Normalize(directionToCollider); // Normalizado
                                                                              // cos(angle) = u.v/||u||.||v||
                float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);

                // Si el angulo es menor que el de vision
                if (angleToCollider < _angle)
                {
                    // Sino hay objetos de la obstacleLayerMask
                    if (!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, _obstacleLayers))
                    {
                        Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                        // Guardamos la referencia del objetivo detectado
                        _detectedTarget = collider;
                        EnemyFollow();
                        Debug.Log("Follow");
                        break;
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, collider.bounds.center, Color.yellow);
                    }
                }
            }
        }
        else
        {
            GetComponent<EnemyFollowPlayer>().enabled = GetComponent<EnemyPatrolB>().enabled = GetComponent<EnemyPatrolA>().enabled = GetComponent<EnemyPatrolA1>().enabled = GetComponent<EnemyPatrolPatio>().enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distance);

        Gizmos.color = Color.magenta;
        Vector3 rightDirection = Quaternion.Euler(0, _angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection * _distance);
        Vector3 leftDirection = Quaternion.Euler(0, -_angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection * _distance);
    }

    public void EnemyFollow()
    {
        GetComponent<EnemyPatrolB>().enabled = GetComponent<EnemyPatrolA>().enabled = GetComponent<EnemyPatrolA1>().enabled = GetComponent<EnemyPatrolPatio>().enabled = false;
        GetComponent<EnemyFollowPlayer>().enabled = true;
    }

    public void ChangeEnabledPatrols(string _location)
    {
        switch (_location)
        {
            case "Patio":
                GetComponent<EnemyPatrolPatio>().enabled = true;
                GetComponent<EnemyPatrolB>().enabled = GetComponent<EnemyPatrolA1>().enabled = GetComponent<EnemyPatrolA>().enabled = false;
                break;

            case "B":
                GetComponent<EnemyPatrolB>().enabled = true;
                GetComponent<EnemyPatrolPatio>().enabled = GetComponent<EnemyPatrolA1>().enabled = GetComponent<EnemyPatrolA>().enabled = false;
                break;

            case "A":
                GetComponent<EnemyPatrolA>().enabled = true;
                GetComponent<EnemyPatrolPatio>().enabled = GetComponent<EnemyPatrolA1>().enabled = GetComponent<EnemyPatrolB>().enabled = false;
                break;

            case "A1":
                GetComponent<EnemyPatrolA1>().enabled = true;
                GetComponent<EnemyPatrolPatio>().enabled = GetComponent<EnemyPatrolB>().enabled = GetComponent<EnemyPatrolA>().enabled = false;
                break;
        }

        Debug.Log(_location);

        locationBeforeFollow = _location;
    }
}
