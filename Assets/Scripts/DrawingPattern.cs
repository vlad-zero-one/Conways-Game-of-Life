using UnityEngine;

public class DrawingPattern : MonoBehaviour
{
    private GameObject cellPrefab, instantiated;

    void OnEnable()
    {
        cellPrefab = Resources.Load("Prefabs/Patterns/CellForDrawing") as GameObject;
        instantiated = Instantiate(cellPrefab, Vector2.zero, Quaternion.identity);
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var patternPosition = new Vector2((int)mousePos.x, (int)mousePos.y);
        instantiated.transform.position = patternPosition;

        if (Input.GetMouseButtonDown(0))
        {
            if (patternPosition.x >= 0
                && patternPosition.x < BorderCreation.borderSize
                && patternPosition.y >= 0
                && patternPosition.y < BorderCreation.borderSize)
            {
                Instantiate(cellPrefab, patternPosition, Quaternion.identity);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(instantiated);
            enabled = false;
        }
    }
}
