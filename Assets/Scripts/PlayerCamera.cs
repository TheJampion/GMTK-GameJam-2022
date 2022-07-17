using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform cameraBoundary;
    public static bool cameraLocked;
    public static Vector2 cameraMin;
    public static Vector2 cameraMax;
    public Camera mainCamera;

    // Update is called once per frame

    public static Vector2 randomPointInsideCameraView()
    {
        float x = Random.Range(cameraMin.x + 1, cameraMax.x - 1);
        float y = Random.Range(cameraMin.y + 0.5f, cameraMin.y + 2f);
        return new Vector2(x, y);
    }

    public static Vector2 randomPointOutsideCameraView()
    {
        float x = Random.Range(cameraMin.x - 1, cameraMax.x + 1);
        float y = Random.Range(cameraMin.y + 0.5f, cameraMin.y + 2f);
        return new Vector2(x, y);
    }

    void Update()
    {
        cameraMin = mainCamera.ViewportToWorldPoint(Vector3.zero);
        cameraMax = mainCamera.ViewportToWorldPoint(Vector3.one);
        if (!cameraLocked)
        {
            cameraBoundary.transform.position = new Vector3(transform.position.x, cameraBoundary.transform.position.y);
        }
    }
}
