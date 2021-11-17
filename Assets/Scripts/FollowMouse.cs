using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Camera cam;
    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        rect.SetPositionAndRotation(new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
    }
    // float ConvertToUnits(float p)
    // {a
    //     float ortho = cam.orthographicSize;
    //     float pixelH = cam.pixelHeight;

    //     return (p * ortho * 2f) / pixelH;
    // }
    // ssaald
}
