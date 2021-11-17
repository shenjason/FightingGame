using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairZoom : MonoBehaviour
{
    public Animator ani;
    public void StartZoom()
    {
        ani.SetTrigger("Zoom");
    }
}
