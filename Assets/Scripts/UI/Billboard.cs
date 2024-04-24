using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject _cam;

    void Awake()
    {
        _cam = GameObject.FindWithTag("MainCamera");
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + _cam.transform.forward);
    }
}
