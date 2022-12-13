using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public Cloud[] clouds;

    public void CreateCloud(Vector3 pos)
    {
        Instantiate(clouds[Random.Range(0, 2)], new Vector2(pos.x + 10f + Random.Range(-2, 2), pos.y + 12f + Random.Range(-2, 2)), Quaternion.identity);
    }

    private void OnEnable()
    {
        CreateCloud(transform.position);
    }
}
