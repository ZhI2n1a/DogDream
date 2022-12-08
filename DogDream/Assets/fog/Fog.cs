using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    Player Player;

    Camera cam;
    public Sprite fog;
    public float speed = 2;
    public bool dead;

    bool _fog = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        dead = Player.GetFucked();
        if (dead)
        {
            if (!_fog)
            {
                transform.position = new Vector3(cam.transform.position.x - 24, cam.transform.position.y, transform.position.z);
            }
            if (transform.position.x < cam.transform.position.x)
            {
                /*transform.position = new Vector3(screenPosition.x, transform.position.y, transform.position.z);*/
                _fog = true;
                transform.Translate(Time.deltaTime * Vector2.right * speed);
            }
        }
    }
}
