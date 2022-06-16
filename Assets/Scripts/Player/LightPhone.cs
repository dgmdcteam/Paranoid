using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPhone : MonoBehaviour
{
    [SerializeField] private GameObject _lightBig, _lightSmall;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lightBig.SetActive((_lightBig.activeSelf == true) ? false : true);
            _lightSmall.SetActive((_lightSmall.activeSelf == true) ? false : true);
        }
    }
}
