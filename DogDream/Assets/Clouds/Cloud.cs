using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{


    Sprite cloud;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
