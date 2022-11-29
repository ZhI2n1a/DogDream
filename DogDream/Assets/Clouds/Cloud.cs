using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{


    Sprite cloud;

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x < -5)
        {
            Destroy(transform.gameObject);
        }
    }
}
