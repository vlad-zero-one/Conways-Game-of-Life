using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class CreatedPatternsSrollView : MonoBehaviour
{
    private GameObject cellPrefab, instantiated;

    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/CustomPatterns/");
        Debug.Log(sprites.Length);
        GameObject content = gameObject;
        foreach(Sprite sprite in sprites)
        {
            GameObject button = new GameObject(sprite.name);
            Image image = button.AddComponent<Image>();
            Button innerButton = button.AddComponent<Button>();

            image.sprite = sprite;
            button.transform.SetParent(content.transform);

            //delegate { DrawPatternFromScroll(sprite.name); }
            innerButton.onClick.AddListener(() => DrawPatternFromScroll(sprite.name));

            //can't change button size, wasted about 4 hours with it...
            //button.gameObject.GetComponent<RectTransform>().rect.Set(0, 0, 300, 300);
            //image.rectTransform.rect.Set(0, 0, 300, 300);

            //var old_rect = button.gameObject.GetComponent<RectTransform>().rect;
            //button.gameObject.GetComponent<RectTransform>().rect.Set(old_rect.x, old_rect.y, 300, 300);

            //var old_rect = button.gameObject.GetComponent<RectTransform>().rect;
            //button.gameObject.GetComponent<RectTransform>().rect.Set(old_rect.x, old_rect.y, 300, 300);

            //image.rectTransform.rect.Set(0, 0, 300, 300);

            //button.GetComponent<RectTransform>().rect.Set(button.GetComponent<RectTransform>().rect.x, button.GetComponent<RectTransform>().rect.y, 300, 300);

        }
    }

    void DrawPatternFromScroll(string patternName)
    {
        cellPrefab = Resources.Load("Prefabs/Patterns/CellForDrawing") as GameObject;
        string[] newFile = File.ReadLines(Application.dataPath + "/Resources/Sprites/CustomPatterns/" + patternName).ToArray();
        
        /*
        Debug.Log(File.Exists(Application.dataPath + "/Resources/Sprites/CustomPatterns/" + patternName));
        Debug.Log(Application.dataPath + "/Resources/Sprites/CustomPatterns/" + patternName);
        Debug.Log(patternName);
        Debug.Log(newFile);
        */
 
        bool[][] patternBoolArray = new bool[newFile.Length][];
        for (int i = 0; i < newFile.Length; i++)
        {
            patternBoolArray[i] = new bool[newFile[i].Length];
            for (int j = 0; j < patternBoolArray[i].Length; j++)
            {
                patternBoolArray[i][j] = newFile[i].Substring(j, 1) == "1" ? true : false;
            }
        }

        /*
        string format = "";
        for (int i = 0; i < patternBoolArray.Length; i++)
        {
            for (int j = 0; j < patternBoolArray[i].Length; j++)
            {
                format += (patternBoolArray[i][j] ? "1" : "0") + " ";
            }
            format += "\n";
        }
        Debug.Log(format);
        */
        instantiated = new GameObject(patternName);
        for (int x = 0; x < patternBoolArray.Length; x++)
        {
            for (int y = 0; y < patternBoolArray[x].Length; y++)
            {
                if (patternBoolArray[x][y])
                {
                    Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity).transform.SetParent(instantiated.transform);
                }
            }
        }
        //instantiated = Instantiate(cellPrefab, Vector2.zero, Quaternion.identity);
    }

    
    void Update()
    {
        if (instantiated)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var patternPosition = new Vector2((int)mousePos.x, (int)mousePos.y);

            instantiated.transform.position = patternPosition;

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(instantiated, patternPosition, Quaternion.identity);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(instantiated);
                //this.enabled = false;
            }
        }
    }
}
