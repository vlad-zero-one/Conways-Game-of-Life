using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PrefabPatternsScrollViewHandler : MonoBehaviour
{
    GameObject instantiated;

    void Start()
    {
        string basePath = "/Resources/Prefabs/Patterns/";
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>("Sprites/Patterns/");
        foreach(var sprite in loadedSprites)
        {
            if (File.Exists(Application.dataPath + basePath + sprite.name + ".prefab"))
            {
                GameObject button = CreateButton(sprite);
                button.transform.SetParent(gameObject.transform.Find("Viewport").Find("Content").transform);
                RectTransform rect = button.GetComponent<RectTransform>();
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
            }
        }
    }

    GameObject CreateButton(Sprite sprite)
    {
        GameObject mainButton = new GameObject(sprite.name);
        Button innerButton = mainButton.AddComponent<Button>();
        Image buttonImage = mainButton.AddComponent<Image>();
        buttonImage.sprite = sprite;
        buttonImage.preserveAspect = true;

        // name apeared on the pattern
        GameObject textForButton = new GameObject("Text");
        Text text = textForButton.AddComponent<Text>();
        text.text = sprite.name;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = ArialFont;
        //text.material = ArialFont.material;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        text.fontSize = 24;
        text.fontStyle = FontStyle.Bold;
        textForButton.transform.SetParent(mainButton.transform);

        innerButton.onClick.AddListener(() => DrawPatternFromScroll(sprite.name));
        return mainButton;
    }

    void DrawPatternFromScroll(string patternName)
    {
        GameObject patternPrefab = Resources.Load("Prefabs/Patterns/" + patternName) as GameObject;
        instantiated = Instantiate(patternPrefab, Vector2.zero, Quaternion.identity);
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
                foreach (Transform cellTransform in instantiated.transform)
                {
                    var cellPos = cellTransform.position;
                    if (cellPos.x >= 0 &&
                        cellPos.x < BorderCreation.borderSize &&
                        cellPos.y >= 0 &&
                        cellPos.y < BorderCreation.borderSize)
                    {
                        Vector3 vec = new Vector3(cellTransform.position.x, cellTransform.position.y);
                        var instCell = Instantiate(cellTransform.gameObject, vec, Quaternion.identity);

                    }
                }

                //Instantiate(instantiated, patternPosition, Quaternion.identity);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(instantiated);
                //this.enabled = false;
            }
        }
    }
}
