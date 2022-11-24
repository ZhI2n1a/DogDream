using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestoyer : MonoBehaviour
{
    public float time;

    void Start()
    {
        StartCoroutine(ObjDestroy());
    }

    private IEnumerator ObjDestroy()
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }
}
