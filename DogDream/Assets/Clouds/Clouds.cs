using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public GameObject[] clouds;
    public float time = 2f;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
