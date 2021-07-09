using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCreation : MonoBehaviour
{
    public GameObject borderCell;

    public static int borderSize;

    public Camera cam;

    void Start()
    {
        GameObject border = new GameObject("Border");

        //Debug.Log(borderCell.name);

        borderSize = AreaChanger.sizeFromApplyButton;

        for (int i = -1; i < borderSize + 1; i++)
        {
            for (int j = -1; j < borderSize + 1; j = j + borderSize + 1)
            {
                var tmp = Instantiate(borderCell, new Vector2(i, j), Quaternion.identity);
                tmp.transform.SetParent(border.transform);
                tmp = Instantiate(borderCell, new Vector2(j, i), Quaternion.identity);
                tmp.transform.SetParent(border.transform);

            }
        }

        cam = GetComponent<Camera>();

        int tmpSizeCam = borderSize / 2;

        cam.orthographicSize = tmpSizeCam;
        cam.transform.position = new Vector3(tmpSizeCam, tmpSizeCam, -10);
    }
}
