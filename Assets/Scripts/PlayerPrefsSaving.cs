using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsSaving : MonoBehaviour
{
    GameObject[] cellsForPattern;

    void OnEnable()
    {
        Button saveButton = GetComponent<Button>();
        string nameForNewPattern = saveButton.transform.Find("InputField").Find("Text").GetComponent<Text>().text;
        SavePattern(nameForNewPattern);
        saveButton.transform.Find("InputField").gameObject.SetActive(false);
        var scrollView = GameObject.Find("Scroll View");
        scrollView.SetActive(false);
        scrollView.SetActive(true);
    }

    void SavePattern(string patternName)
    {
        // if PlayerPrefs hasn't string with names of the saved patterns, create it
        if (!PlayerPrefs.HasKey("SavedPatternsName"))
        {
            PlayerPrefs.SetString("SavedPatternsName", patternName);
        }
        // if it has, check if it contain current pattern name, player trying to save
        else
        {
            string alreadySavedNames = PlayerPrefs.GetString("SavedPatternsName");
            if(alreadySavedNames.Contains(patternName))
            {
                // here we drop a window with the message that the pattern with this name already exists
                throw new System.Exception("The pattern with this name already exists!");
            }
            else
            {
                PlayerPrefs.SetString("SavedPatternsName", alreadySavedNames + "," + patternName);
            }
        }

        // Here we go, saving the pattern
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
        }

        bool[][] patternBoolArray = new bool[maxX - minX + 1][];
        for (int i = 0; i < patternBoolArray.Length; i++)
        {
            patternBoolArray[i] = new bool[maxY - minY + 1];
        }
        foreach (var cell in cellsForPattern)
        {
            patternBoolArray[(int)cell.transform.position.x - minX][(int)cell.transform.position.y - minY] = true;
        }

        string stringForSaving = "";
        for (int x = 0; x < patternBoolArray.Length; x++)
        {
            for (int y = 0; y < patternBoolArray[0].Length; y++)
            {
                stringForSaving += (patternBoolArray[x][y] ? "1" : "0");
            }
            stringForSaving += "|";
        }
        stringForSaving = stringForSaving.TrimEnd('|');

        PlayerPrefs.SetString(patternName, stringForSaving);
        PlayerPrefs.Save();
    }
}
