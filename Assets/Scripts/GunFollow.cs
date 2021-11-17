using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour
{
    public FollowCam Cam;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.rotation = Cam.angle * Mathf.Rad2Deg;
        transform.localScale = new Vector3(1, FlipNum(rb.rotation), 1);
    }

    int FlipNum(float dir)
    {
        int flip;
        float dirnormal = dir + 90f;
        if (dirnormal > 0 && dirnormal < 180)
            flip = 1;
        else
            flip = -1;
        return flip;
    }
}
