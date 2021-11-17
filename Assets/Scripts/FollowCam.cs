using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform FollowTarget;
    public Renderer FollowRenderer;
    public float ViewRange, angle;
    public bool MouseFollow = true;
    public Camera cam;
    private Camera Mycam;
    private float OffsetX, OffsetY, dx, dy;
    Vector2 mousePos;


    void Awake()
    {
        OffsetX = 0f;
        OffsetY = 0f;
        Mycam = GetComponent<Camera>();
        cam.enabled = false;
    }
    void Update()
    {
        transform.position = new Vector3(FollowTarget.position.x + OffsetX, FollowTarget.position.y + OffsetY, -10f);
        if (MouseFollow)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            dx = mousePos.x - FollowTarget.position.x;
            dy = mousePos.y - FollowTarget.position.y;
            CamF();
            ScaleCam();
        }

    }

    void CamF()
    {
        float dis = Vector2.Distance(mousePos,FollowTarget.position);
        if (dis >= ViewRange)
        {
            angle = FindAngle(FollowTarget.position, mousePos);
            OffsetX = ViewRange * Mathf.Cos(angle);
            OffsetY = ViewRange * Mathf.Sin(angle);
        }
        else
        {
            angle = FindAngle(FollowTarget.position, mousePos);
            OffsetX = dx;
            OffsetY = dy;
        }
    }

    float FindAngle(Vector2 pos1, Vector2 pos2)
    {
        Vector2 lookdir = pos2 - pos1;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x);
        return angle;

    }

    void ScaleCam()
    {
        if (FollowRenderer.isVisible)
        {
            Mycam.fieldOfView -= 1f;
        }
        else
        {
            Mycam.fieldOfView += 1f;
        }
    }
}
