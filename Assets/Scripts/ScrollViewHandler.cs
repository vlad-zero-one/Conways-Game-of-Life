using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewHandler : MonoBehaviour
{
    public int pixelsPerUnit = 100;
    Dictionary<string, bool[][]> loadedPatterns;
    GameObject instantiated;

    void OnEnable()
    {
        loadedPatterns = PlayerPrefsLoading.LoadAllSavedPatterns();
        if (loadedPatterns != null)
        {
            Debug.Log(loadedPatterns.Count);

            foreach (var loadedPattern in loadedPatterns)
            {
                Debug.Log(loadedPattern.Key);
            }
            foreach (var loadedPattern in loadedPatterns)
            {
                GameObject patternButton = CreateButton(loadedPattern);
                //Debug.Log(gameObject.transform.Find("Content").name);
                patternButton.transform.SetParent(transform.Find("Viewport").Find("Content"));
                RectTransform rect = patternButton.GetComponent<RectTransform>();
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
            }
        }
    }

    GameObject CreateButton(KeyValuePair<string, bool[][]> nameArrayPair)
    {
        GameObject patternButton = new GameObject(nameArrayPair.Key);
        Image image = patternButton.AddComponent<Image>();
        Button innerButton = patternButton.AddComponent<Button>();

        // name apeared on the pattern
        GameObject textForButton = new GameObject("Text");
        Text text = textForButton.AddComponent<Text>();
        text.text = nameArrayPair.Key;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = ArialFont;
        //text.material = ArialFont.material;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        text.fontSize = 24;
        text.fontStyle = FontStyle.Bold;
        

        textForButton.transform.SetParent(patternButton.transform);

        var old_rect = patternButton.GetComponent<RectTransform>().rect;
        patternButton.GetComponent<RectTransform>().rect.Set(old_rect.x, old_rect.y, 300, 300);

        // creating new sprite for button
        int spriteWidth = pixelsPerUnit;
        var patternBoolArray = nameArrayPair.Value;
        Texture2D combinedTexture = new Texture2D(spriteWidth * patternBoolArray.Length, spriteWidth * patternBoolArray[0].Length);
        Color32 alphaCol = new Color32(0, 0, 0, 0);
        Color32 whiteCol = new Color32(255, 255, 255, 255);
        Color32[] whiteSprite = new Color32[spriteWidth * spriteWidth];
        Color32[] alphaSprite = new Color32[spriteWidth * spriteWidth];
        for (int i = 0; i < pixelsPerUnit * pixelsPerUnit; i++)
        {
            alphaSprite[i] = alphaCol;
            whiteSprite[i] = whiteCol;
        }

        for (int i = 0; i < patternBoolArray.Length; i++)
        {
            for (int j = 0; j < patternBoolArray[0].Length; j++)
            {
                if (patternBoolArray[i][j] == true)
                {
                    combinedTexture.SetPixels32(
                    i * spriteWidth,
                    j * spriteWidth,
                    spriteWidth,
                    spriteWidth,
                    whiteSprite);
                }
                else
                {
                    combinedTexture.SetPixels32(
                    i * spriteWidth,
                    j * spriteWidth,
                    spriteWidth,
                    spriteWidth,
                    alphaSprite);
                }
            }
        }
        combinedTexture.Apply();

        var patternSprite = Sprite.Create(
            combinedTexture,
            new Rect(0.0f, 0.0f, combinedTexture.width, combinedTexture.height),
            new Vector2(0.5f, 0.5f), 100.0f);

        image.sprite = patternSprite;
        image.preserveAspect = true;

        //delegate { DrawPatternFromScroll(sprite.name); }
        innerButton.onClick.AddListener(() => DrawPatternFromScroll(nameArrayPair.Key));

        return patternButton;
    }

    void DrawPatternFromScroll(string patternName)
    {
        GameObject cellPrefab = Resources.Load("Prefabs/Patterns/CellForDrawing") as GameObject;

        bool[][] patternBoolArray = loadedPatterns[patternName];

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
                    //Instantiate(instantiated, patternPosition, Quaternion.identity);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(instantiated);
                //this.enabled = false;
            }
        }
    }

}
