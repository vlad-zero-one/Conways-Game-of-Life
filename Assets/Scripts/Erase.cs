using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Erase : MonoBehaviour
{
    private GameObject eraser;
    private GameObject[] cellsOnArea;

    void OnEnable()
    {
        eraser = Resources.Load("Prefabs/Patterns/CellForErasing") as GameObject;
        eraser = Instantiate(eraser, Vector2.zero, Quaternion.identity);
        eraser.GetComponent<SpriteRenderer>().color = GameObject.Find("Eraser").GetComponent<Image>().color;
        eraser.gameObject.tag = "Eraser";
        cellsOnArea = GameObject.FindGameObjectsWithTag("Cell");
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var patternPosition = new Vector2((int)mousePos.x, (int)mousePos.y);
        eraser.transform.position = patternPosition;

        if (Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < cellsOnArea.Length; i++)
            {
                var cell = cellsOnArea[i];
                if (cell.transform.position.x == eraser.transform.position.x
                    && cell.transform.position.y == eraser.transform.position.y)
                {
                    var tmp = cellsOnArea[i];
                    cellsOnArea = cellsOnArea.Where(obj => obj != tmp).ToArray();
                    Destroy(tmp);
                    break;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(eraser);
            enabled = false;
        }
    }
}
