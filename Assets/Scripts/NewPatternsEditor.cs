// all of the following code should be commented before buiding the game because of the UnityEditor
// code should be used only for adding patterns inside the Unity Editor
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class NewPatternsEditor : MonoBehaviour
{
    Button saveButton;
    GameObject[] cellsForPattern;
    GameObject parentObject;
    Sprite cellSprite;
    int spriteWidth = 100;

    void OnEnable()
    {
        /*
        GameObject button = new GameObject("TESTBUT");
        button.transform.SetParent(GameObject.Find("Canvas").transform);
        button.AddComponent<Button>();
        var image = button.AddComponent<Image>();
        image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd");
        image.color = Color.blue;
        RectTransform rect = button.GetComponent<RectTransform>();
        rect.localPosition = new Vector3(-50, -50);
        //rect.ForceUpdateRectTransforms();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);

        GameObject inputField = new GameObject("InputField");
        image = inputField.AddComponent<Image>();
        image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/InputFieldBackground.psd");
        inputField.AddComponent<InputField>();
        //inputField.AddComponent<Text>().text = "sfsd";
        inputField.transform.SetParent(button.transform);
        */
        /*
        // name apeared on the pattern
        GameObject textForButton = new GameObject("Text");
        Text text = textForButton.AddComponent<Text>();
        text.text = "";
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = ArialFont;
        //text.material = ArialFont.material;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        text.fontSize = 24;
        text.fontStyle = FontStyle.Bold;
        */

        //cellSprite = Resources.Load("/Sprites/Cell") as Sprite;
        //cellSprite = Instantiate(cellSprite, Vector3.zero, Quaternion.identity);
        cellSprite = Resources.Load<Sprite>("Sprites/Cell");
        Debug.Log(cellSprite.texture);
        //cellSprite = Instantiate(cellSprite, Camera.main.transform);
        saveButton = GetComponent<Button>();
        string nameForNewPattern = saveButton.transform.Find("InputField").Find("Text").GetComponent<Text>().text;
        saveButton.transform.Find("InputField").gameObject.SetActive(false);
        parentObject = new GameObject(nameForNewPattern);
        cellsForPattern = GameObject.FindGameObjectsWithTag("Cell");
        // Here we need minimum and maximum x, y for understand the size of our future pattern
        int minX = (int)cellsForPattern[0].transform.position.x;
        int maxX = (int)cellsForPattern[0].transform.position.x;
        int minY = (int)cellsForPattern[0].transform.position.y;
        int maxY = (int)cellsForPattern[0].transform.position.y;
        foreach (var cell in cellsForPattern)
        {
            if (cell.transform.position.x > maxX) maxX = (int)cell.transform.position.x;
            else if (cell.transform.position.x < minX) minX = (int)cell.transform.position.x;
            if (cell.transform.position.y > maxY) maxY = (int)cell.transform.position.y;
            else if (cell.transform.position.y < minY) minY = (int)cell.transform.position.y;
            //cell.transform.SetParent(parentObject.transform);
        }

        Debug.Log("X: " + minX + " " + maxX);
        Debug.Log("Y: " + minY + " " + maxY);


        bool[][] patternBoolArray = new bool[maxX - minX + 1][];
        for (int i = 0; i < patternBoolArray.Length; i++)
        {
            patternBoolArray[i] = new bool[maxY - minY + 1];
        }

        string formatter = "";
        for (int i = 0; i < patternBoolArray.Length; i++)
        {
            for (int j = 0; j < patternBoolArray[0].Length; j++)
            {
                formatter += (patternBoolArray[i][j] ? "1" : "0") + " ";
            }
            formatter += "\n";
        }
        Debug.Log(formatter);

        Texture2D combinedTexture = new Texture2D(spriteWidth * patternBoolArray.Length, spriteWidth * patternBoolArray[0].Length);
        /*
        for (int x = 0; x < patternBoolArray.Length; x++)
        {
            for (int y = 0; y < patternBoolArray[0].Length; y++)
            {
                combinedTexture.SetPixels32(x * spriteWidth, y * spriteWidth, spriteWidth, spriteWidth, cellSprite.texture.GetPixels32());
            }
        }
        */
        
        foreach (var cell in cellsForPattern)
        {
            cell.transform.SetParent(parentObject.transform);
            try
            {
                patternBoolArray[(int)cell.transform.position.x - minX][(int)cell.transform.position.y - minY] = true;
            }
            catch (System.IndexOutOfRangeException)
            {
                int tmp1 = (int)cell.transform.position.x - minX;
                int tmp2 = (int)cell.transform.position.y - minY;
                Debug.Log(tmp1 + " " + tmp2);
                Debug.LogError("!!!!");
            }
        }

        formatter = "";
        for (int i = patternBoolArray.Length - 1; i > 0; i--)
        {
            for (int j = 0; j < patternBoolArray[0].Length; j++)
            {
                int indSum = i + j;
                formatter += (patternBoolArray[i][j] ? "1" : "0") + " ";
            }
            formatter += "\n";
        }
        Debug.Log(formatter);

        Color32 alphaCol = new Color32(0, 0, 0, 0);
        Color32 whiteCol = new Color32(255, 255, 255, 255);

        Color32[] whiteSprite = new Color32[spriteWidth * spriteWidth];
        Color32[] alphaSprite = new Color32[spriteWidth * spriteWidth];
        for (int i = 0; i < spriteWidth * spriteWidth; i++)
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
                    //cellSprite.texture.GetPixels32());
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

        var finalSprite = Sprite.Create(
            combinedTexture,
            new Rect(0.0f, 0.0f, combinedTexture.width, combinedTexture.height),
            new Vector2(0.5f, 0.5f), 100.0f);

        string basePath = "/Resources/Sprites/Patterns/";
        if (!Directory.Exists(Application.dataPath + basePath))
        {
            Directory.CreateDirectory(Application.dataPath + basePath);
        }
        File.WriteAllBytes(
            Application.dataPath + basePath + nameForNewPattern + ".PNG",
            finalSprite.texture.EncodeToPNG());

        Sprite savedAndLoadedSprite = Resources.Load<Sprite>("Sprites/Patterns/" + nameForNewPattern + ".PNG");

        Debug.Log("Sprites/Patterns/" + nameForNewPattern);
        Debug.Log(savedAndLoadedSprite);


        saveButton = GetComponent<Button>();
        saveButton.transform.Find("InputField").gameObject.SetActive(false);
        parentObject = new GameObject(nameForNewPattern);
        cellsForPattern = GameObject.FindGameObjectsWithTag("Cell");
        foreach (var cell in cellsForPattern)
        {
            var oldCellPos = cell.transform.position;
            cell.transform.position = new Vector3((int)oldCellPos.x - minX, (int)oldCellPos.y - minY);
            Debug.Log("posX: " + cell.transform.position.x + " posY: " + cell.transform.position.y);
            cell.transform.SetParent(parentObject.transform);
        }
        PrefabUtility.SaveAsPrefabAsset(parentObject, "Assets/Resources/Prefabs/Patterns/" + nameForNewPattern + ".prefab");
        var oldPrefabPos = parentObject.transform.position;
        parentObject.transform.position = new Vector3((int)oldPrefabPos.x + minX, (int)oldPrefabPos.y + minY);

        /*
        var textFile = File.CreateText(Application.dataPath + basePath + nameForNewPattern);
        // матрица в файле будет "повёрнута" на 90 градусов по часовой стрелке относительно исходного паттерна
        // т.е. ось X будет указывать вниз, а ось Y вправо
        for (int x = 0; x < patternBoolArray.Length; x++)
        {
            for (int y = 0; y < patternBoolArray[0].Length; y++)
            {
                textFile.Write(patternBoolArray[x][y] ? "1" : "0");

            }
            textFile.WriteLine();
        }
        textFile.Close();
        var newFile = File.ReadLines(Application.dataPath + basePath + nameForNewPattern);
        foreach (var line in newFile)
        {
            Debug.Log(line);
        }
        */

        Sprite sss = Resources.Load<Sprite>("Sprites/Patterns/" + nameForNewPattern + ".PNG");

        Debug.Log(sss);

        enabled = false;

    }
}
    // It works but, only in development mode, It can't be built because of UnityEditor in the code
    /*
    void Start()
    {
        saveButton = GetComponent<Button>();
        string nameForNewPattern = saveButton.transform.Find("InputField").Find("Text").GetComponent<Text>().text;
        saveButton.transform.Find("InputField").gameObject.SetActive(false);
        parentObject = new GameObject(nameForNewPattern);
        cellsForPattern = GameObject.FindGameObjectsWithTag("Cell");
        foreach (var cell in cellsForPattern)
        {
            cell.transform.SetParent(parentObject.transform);
        }
        UnityEditor.PrefabUtility.SaveAsPrefabAsset(parentObject, "Assets/Resources/Prefabs/Patterns/" + nameForNewPattern + ".prefab");
    }
    */

/*
void Start()
{
    //cellSprite = Resources.Load("/Sprites/Cell") as Sprite;
    //cellSprite = Instantiate(cellSprite, Vector3.zero, Quaternion.identity);
    cellSprite = Resources.Load<Sprite>("Sprites/Cell");
    Debug.Log(cellSprite.texture);
    //cellSprite = Instantiate(cellSprite, Camera.main.transform);
    saveButton = GetComponent<Button>();
    string nameForNewPattern = saveButton.transform.Find("InputField").Find("Text").GetComponent<Text>().text;
    saveButton.transform.Find("InputField").gameObject.SetActive(false);
    parentObject = new GameObject(nameForNewPattern);
    cellsForPattern = GameObject.FindGameObjectsWithTag("Cell");
    // Here we need minimum and maximum x, y for understand the size of our future pattern
    int minX = (int)cellsForPattern[0].transform.position.x;
    int maxX = (int)cellsForPattern[0].transform.position.x;
    int minY = (int)cellsForPattern[0].transform.position.y;
    int maxY = (int)cellsForPattern[0].transform.position.y;
    foreach (var cell in cellsForPattern)
    {
        if (cell.transform.position.x > maxX) maxX = (int)cell.transform.position.x;
        else if (cell.transform.position.x < minX) minX = (int)cell.transform.position.x;
        if (cell.transform.position.y > maxY) maxY = (int)cell.transform.position.y;
        else if (cell.transform.position.y < minY) minY = (int)cell.transform.position.y;
        //cell.transform.SetParent(parentObject.transform);
    }

    Debug.Log("X: " + minX + " " + maxX);
    Debug.Log("Y: " + minY + " " + maxY);


    bool[][] patternBoolArray = new bool[maxX - minX + 1][];
    for (int i = 0; i < patternBoolArray.Length; i++)
    {
        patternBoolArray[i] = new bool[maxY - minY + 1];
    }

    string formatter = "";
    for (int i = 0; i < patternBoolArray.Length; i++)
    {
        for (int j = 0; j < patternBoolArray[0].Length; j++)
        {
            formatter += (patternBoolArray[i][j] ? "1" : "0") + " ";
        }
        formatter += "\n";
    }
    Debug.Log(formatter);

    Texture2D combinedTexture = new Texture2D(spriteWidth * patternBoolArray.Length, spriteWidth * patternBoolArray[0].Length);
    /*
    for (int x = 0; x < patternBoolArray.Length; x++)
    {
        for (int y = 0; y < patternBoolArray[0].Length; y++)
        {
            combinedTexture.SetPixels32(x * spriteWidth, y * spriteWidth, spriteWidth, spriteWidth, cellSprite.texture.GetPixels32());
        }
    }
    */
/*
foreach (var cell in cellsForPattern)
{
    cell.transform.SetParent(parentObject.transform);
    try
    {
        patternBoolArray[(int)cell.transform.position.x - minX][(int)cell.transform.position.y - minY] = true;
    }
    catch (System.IndexOutOfRangeException)
    {
        int tmp1 = (int)cell.transform.position.x - minX;
        int tmp2 = (int)cell.transform.position.y - minY;
        Debug.Log(tmp1 + " " + tmp2);
        Debug.LogError("!!!!");
    }
}

formatter = "";
for (int i = patternBoolArray.Length - 1; i > 0; i--)
{
    for (int j = 0; j < patternBoolArray[0].Length; j++)
    {
        int indSum = i + j;
        formatter += (patternBoolArray[i][j] ? "1" : "0") + " ";
    }
    formatter += "\n";
}
Debug.Log(formatter);

Color32 alphaCol = new Color32(0, 0, 0, 0);
Color32 whiteCol = new Color32(255, 255, 255, 255);

Color32[] whiteSprite = new Color32[spriteWidth * spriteWidth];
Color32[] alphaSprite = new Color32[spriteWidth * spriteWidth];
for (int i = 0; i < spriteWidth * spriteWidth; i++)
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
            //cellSprite.texture.GetPixels32());
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

var finalSprite = Sprite.Create(
    combinedTexture,
    new Rect(0.0f, 0.0f, combinedTexture.width, combinedTexture.height),
    new Vector2(0.5f, 0.5f), 100.0f);

string basePath = "/Resources/Sprites/CustomPatterns/";
if (!Directory.Exists(Application.dataPath + basePath))
{
    Directory.CreateDirectory(Application.dataPath + basePath);
}
File.WriteAllBytes(
    Application.dataPath + basePath + nameForNewPattern + ".PNG",
    finalSprite.texture.EncodeToPNG());

Sprite savedAndLoadedSprite = Resources.Load<Sprite>("Sprites/CustomPatterns/" + nameForNewPattern);

Debug.Log("Sprites/CustomPatterns/" + nameForNewPattern);
Debug.Log(savedAndLoadedSprite);



var textFile = File.CreateText(Application.dataPath + basePath + nameForNewPattern);
// матрица в файле будет "повёрнута" на 90 градусов по часовой стрелке относительно исходного паттерна
// т.е. ось X будет указывать вниз, а ось Y вправо
for (int x = 0; x < patternBoolArray.Length; x++)
{
    for (int y = 0; y < patternBoolArray[0].Length; y++)
    {
        textFile.Write(patternBoolArray[x][y] ? "1" : "0");

    }
    textFile.WriteLine();
}
textFile.Close();
var newFile = File.ReadLines(Application.dataPath + basePath + nameForNewPattern);
foreach (var line in newFile)
{
    Debug.Log(line);
}


Sprite sss = Resources.Load<Sprite>("Sprites/CustomPatterns/" + nameForNewPattern);

Debug.Log(sss);

}

*/
      