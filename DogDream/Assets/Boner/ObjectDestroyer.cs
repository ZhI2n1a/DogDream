using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public GameObject objDestrPoint;

    // Start is called before the first frame update
    void Start()
    {
        objDestrPoint = GameObject.Find("DestrP");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < objDestrPoint.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
