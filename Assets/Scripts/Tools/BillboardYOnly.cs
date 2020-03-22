using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardYOnly : MonoBehaviour
{
    private Camera cam;

    void Update()
    {
        if (cam == null && FindObjectOfType<Camera>() != null)
            cam = FindObjectOfType<Camera>();

        Vector3 v = cam.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cam.transform.position - v);
    }
}
