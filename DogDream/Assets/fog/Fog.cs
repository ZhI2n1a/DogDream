using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    Transform Player;
    public bool _playerFell = false;
    public float offsetX;
    int count;


    float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerFell)
        {
            count++;
            _playerFell = false;
        }

        if (count == 1)
        {
            transform.position = new Vector2(Player.position.x - offsetX, Player.position.y);
        }

        if (count == 2)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Game Over!");
        }
    }
}
