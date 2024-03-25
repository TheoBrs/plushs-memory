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
        MoveTo();
    }

    public void MoveTo()
    {
        if ((transform.position - newPosition).magnitude < 0.01f)
            transform.position = newPosition;
        else
        {
            Vector3 directeur = movementSpeed * Time.deltaTime * (newPosition - transform.position).normalized;

            transform.position += directeur;
        }
    }
}
