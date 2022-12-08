using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime);
    }
}
