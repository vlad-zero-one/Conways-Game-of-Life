using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPattern : MonoBehaviour
{
    private GameObject cellPrefab, instantiated;


    void OnEnable()
    {
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        cellPrefab = Resources.Load("Prefabs/Patterns/CellForDrawing") as GameObject;
        instantiated = Instantiate(cellPrefab, Vector2.zero, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var patternPosition = new Vector2((int)mousePos.x, (int)mousePos.y);

        instantiated.transform.position = patternPosition;

        if (Input.GetMouseButtonDown(0))
        {
            
            Instantiate(cellPrefab, patternPosition, Quaternion.identity);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(instantiated);
            this.enabled = false;
        }
    }
}
