using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private static int cameraZoomSpeed = 10;
    private static int minCameraSize = 5;
    private static int maxCameraSize = 100;
    private static float weight = 0.5f;
    /*
    public static void Zoom()
    {
        maxCameraSize = BorderCreation.borderSize;
        Camera cam = Camera.main;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 defaultPosition = new Vector3(maxCameraSize, maxCameraSize, -10);


        var futureSize = Camera.main.orthographicSize - (int)(Input.GetAxis("Mouse ScrollWheel") * cameraZoomSpeed);

        if (Input.GetAxis("Mouse ScrollWheel") > 0  && futureSize >= minCameraSize)
        {
            Camera.main.orthographicSize = futureSize;
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, mousePos, 1f);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && futureSize <= maxCameraSize)
        {
            Camera.main.orthographicSize = futureSize;
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, defaultPosition, 1f);
        }   
    }
    */

    public static void Zoom()
    {
        Camera cam = Camera.main;
        maxCameraSize = BorderCreation.borderSize;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ZoomOrthoCamera(cam, Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
        }

        // Scoll back
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ZoomOrthoCamera(cam, Camera.main.ScreenToWorldPoint(Input.mousePosition), -1);
        }
    }


    static void ZoomOrthoCamera(Camera cam, Vector3 zoomTowards, float amount)
    {
        // Calculate how much we will have to move towards the zoomTowards position
        float multiplier = (1.0f / cam.orthographicSize * amount);

        // Move camera
        if (cam.transform.position.x > 0 && 
            cam.transform.position.x < maxCameraSize &&
            cam.transform.position.y > 0 &&
            cam.transform.position.y < maxCameraSize)
        {
            cam.transform.position += (zoomTowards - cam.transform.position) * multiplier;
        }
        /*
        cam.transform.position.Set(
            Mathf.Clamp(cam.transform.position.x + (zoomTowards.x - cam.transform.position.x) * multiplier, minCameraSize, maxCameraSize),
            Mathf.Clamp(cam.transform.position.y + (zoomTowards.y - cam.transform.position.y) * multiplier, minCameraSize, maxCameraSize),
            -10);
        */

        // Zoom camera
        cam.orthographicSize -= amount;

        // Limit zoom
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minCameraSize, maxCameraSize);
    }
}
