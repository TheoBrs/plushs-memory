using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    GameObject cam;

    void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
