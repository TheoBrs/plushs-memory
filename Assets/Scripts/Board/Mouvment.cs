using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 newPosition;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveTo();
    }

    public void moveTo()
    {
        
        if ((transform.position - newPosition).magnitude < 0.01f)
            transform.position = newPosition;
        else
        {
            Vector3 directeur = (newPosition - transform.position).normalized * Time.deltaTime * movementSpeed;

            transform.position += directeur;
        }
    }
}
