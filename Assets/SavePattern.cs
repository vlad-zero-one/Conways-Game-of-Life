using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePattern : MonoBehaviour
{
    Button saveButton;
    GameObject[] cellsForPattern;
    GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        saveButton = GetComponent<Button>();
        string nameForNewPattern = saveButton.transform.Find("InputField").Find("Text").GetComponent<Text>().text;
        saveButton.transform.Find("InputField").gameObject.SetActive(false);
        //Object prefab = UnityEditor.EditorUtility.CreateEmptyPrefab("Assets/Prefabs/Patterns/" + nameForNewPattern + ".prefab");
        //EditorUtility.ReplacePrefab(t.gameObject, prefab, ReplacePrefabOptions.ConnectToPrefab);

        parentObject = new GameObject(nameForNewPattern);

        //saveButton.onClick.AddListener(SavingMethod);

        Debug.Log("Entered");
        cellsForPattern = GameObject.FindGameObjectsWithTag("Cell");
        foreach (var cell in cellsForPattern)
        {
            cell.transform.SetParent(parentObject.transform);
        }
        UnityEditor.PrefabUtility.SaveAsPrefabAsset(parentObject, "Assets/Resources/Prefabs/Patterns/" + nameForNewPattern + ".prefab");
    }
}
