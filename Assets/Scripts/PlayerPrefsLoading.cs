using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsLoading : MonoBehaviour
{
    // We are returning loading the bool[][] that describes saved pattern, for we can use it both for button sprite and for game pattern
    static bool[][] LoadPattern(string name)
    {
        string stringPattern = PlayerPrefs.GetString(name);

        string[] linesArray = stringPattern.Split('|');
        string line;

        bool[][] patternBoolArray = new bool[linesArray.Length][];
        for (int i = 0; i < linesArray.Length; i++)
        {
            line = linesArray[i];
            patternBoolArray[i] = new bool[line.Length];
            for (int j = 0; j < line.Length; j++)
            {
                if(line[j] == '1')
                {
                    patternBoolArray[i][j] = true;
                }
                else
                {
                    patternBoolArray[i][j] = false;
                }
            }
        }
        return patternBoolArray;
    }

    public static Dictionary<string, bool[][]> LoadAllSavedPatterns()
    {
        if (PlayerPrefs.HasKey("SavedPatternsName"))
        {
            Dictionary<string, bool[][]> allPatterns = new Dictionary<string, bool[][]>();
            bool[][] patternBoolArray;

            string[] patternNames = PlayerPrefs.GetString("SavedPatternsName").Split(',');

            Debug.Log(PlayerPrefs.GetString("SavedPatternsName"));

            foreach (string patternName in patternNames)
            {
                patternBoolArray = LoadPattern(patternName);
                allPatterns.Add(patternName, patternBoolArray);
            }

            return allPatterns;
        }
        else
        {
            return null;
        }
    }

    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }

}
