using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public GameObject[] clouds;
    public float time = 2f;
    float timer = 0f;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Instantiate(clouds[Random.Range(0,2)], new Vector2(transform.position.x, transform.position.y + Random.Range(-1, 2)), Quaternion.identity);
            timer = time;
        }
    }
}
