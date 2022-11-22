using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawmObs : MonoBehaviour
{
    public GameObject prefab;
    public float delay;
    public float timerDelay = 2f;

    void Start()
    {
        delay = timerDelay;
    }


    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0) 
        {
            Transform.Instantiate(prefab, transform.position, transform.rotation);
            delay = timerDelay;
        }
    }
}
