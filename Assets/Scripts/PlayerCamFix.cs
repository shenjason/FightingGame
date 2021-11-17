using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamFix : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
