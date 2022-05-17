using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] private float _distance, _angle;
    [SerializeField] private LayerMask _targetLayers, _obstacleLayers;
    [SerializeField] private Collider _detectedTarget;
    [SerializeField] private Collider[] _colliders;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
                    GetComponent<EnemyPatrolB>().enabled = false;
                    GetComponent<EnemyFollowPlayer>().enabled = true;
                    break;
                }
                else
                {
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.yellow);
                }
            }
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
}
