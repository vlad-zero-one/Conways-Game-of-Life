// following code should be commented before buiding the game because of the UnityEditor
// code should be used only for adding patterns inside the Unity Editor

using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class NewPatternsEditor : MonoBehaviour
{
    /*
    Button saveButton;
    GameObject[] cellsForPattern;
    GameObject parentObject;
    Sprite cellSprite;
    int spriteWidth = 100;

    void OnEnable()
    {
        cellSprite = Resources.Load<Sprite>("Sprites/Cell");
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

        bool[][] patternBoolArray = new bool[maxX - minX + 1][];
        for (int i = 0; i < patternBoolArray.Length; i++)
        {
            patternBoolArray[i] = new bool[maxY - minY + 1];
        }
        Texture2D combinedTexture = new Texture2D(spriteWidth * patternBoolArray.Length, spriteWidth * patternBoolArray[0].Length);
        foreach (var cell in cellsForPattern)
        {
            cell.transform.SetParent(parentObject.transform);
            patternBoolArray[(int)cell.transform.position.x - minX][(int)cell.transform.position.y - minY] = true;
        }


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

        //Sprite savedAndLoadedSprite = Resources.Load<Sprite>("Sprites/Patterns/" + nameForNewPattern + ".PNG");

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
        enabled = false;
    }
    */
}
