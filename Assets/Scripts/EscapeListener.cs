using UnityEngine;

public class EscapeListener : MonoBehaviour
{
    Canvas mainCanvas, escapeCanvas;

    void Start()
    {
        mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        escapeCanvas = GameObject.Find("EscapeCanvas").GetComponent<Canvas>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject[] inputFields = GameObject.FindGameObjectsWithTag("InputField");
            if (inputFields.Length != 0)
            {
                foreach(var inputField in inputFields)
                {
                    inputField.SetActive(false);
                }
            }
            else
            {
                SwitchCanvas();
            }
        }
    }

    public void SwitchCanvas()
    {
        if (mainCanvas.enabled && !escapeCanvas.enabled)
        {
            mainCanvas.enabled = false;
            escapeCanvas.enabled = true;
        }
        else if (!mainCanvas.enabled && escapeCanvas.enabled)
        {
            mainCanvas.enabled = true;
            escapeCanvas.enabled = false;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RemoveBorder()
    {
        GameObject border = GameObject.Find("Border");
        if(border)
        {
            Destroy(border);
        }
    }

    public void ClearField()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
        foreach (GameObject cell in cells)
        {
            Destroy(cell);
        }
    }
}
