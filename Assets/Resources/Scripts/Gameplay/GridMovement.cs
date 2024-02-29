using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GridMovement : MonoBehaviour
{
    private Vector3 touchPosWorld;
    private Vector3 position;
    private float width;
    private float height;

    public int gridWidth;
    public int gridHeight;
    private Material gridMat;
    private Material selectedGridMat;

    private GameObject Player;
    private GameObject selectedGridCell;

    void Awake()
    {
        Player = GameObject.Find("Player");

        gridMat = Resources.Load("Materials/grid", typeof(Material)) as Material;
        selectedGridMat = Resources.Load("Materials/selectedGrid", typeof(Material)) as Material;

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Vector3 cellPos = new Vector3(i - gridWidth / 2.0f + 0.5f, 0.01f, j - gridHeight / 2.0f + 0.5f);
                GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                plane.transform.localPosition = cellPos;
                plane.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                plane.GetComponent<Renderer>().sharedMaterial = gridMat;
                plane.transform.name = "gridCell";
            }
        }

        width = Screen.width / 2.0f;
        height = Screen.height / 2.0f;

        // Position used for the cube.
        position = new Vector3(0.0f, 0.0f, 0.0f);
    }


    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                //Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        GameObject touchedObject = hit.transform.gameObject;
                        if (touchedObject.transform.name == "gridCell")
                        {
                            if (selectedGridCell != null)
                                selectedGridCell.GetComponent<Renderer>().sharedMaterial = gridMat;

                            touchedObject.GetComponent<Renderer>().sharedMaterial = selectedGridMat;
                            selectedGridCell = touchedObject;
                            Player.GetComponent<Movement>().newPosition = (touchedObject.transform.position + new Vector3(0, 0.36f, 0));
                        }
                    }
                }
            }
        }
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 touchPosition = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2;

                Vector3 newCamPos = new Vector3(-touchPosition.x, -touchPosition.y, -10);
                newCamPos.x = (newCamPos.x + width) / width;
                newCamPos.y = (newCamPos.y + height) / height;
                Camera.main.transform.localPosition = newCamPos;
            }
        }
    }
}
