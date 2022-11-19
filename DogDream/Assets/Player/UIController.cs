using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    Player player;
    Text distance;


    private void Awake()
    {
        player = GameObject.Find("Игрок").GetComponent<Player>();
        distance = GameObject.Find("Distance").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        distance.text = Mathf.FloorToInt(player.distance) + " m";
    }
}
