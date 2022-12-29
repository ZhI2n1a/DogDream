using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    Player player;
    Text distance;
    Text bones;
    Text highScore;

    private void Awake()
    {
        highScore = GameObject.Find("HighScore").GetComponent<Text>();
        player = GameObject.Find("Игрок").GetComponent<Player>();
        distance = GameObject.Find("Distance").GetComponent<Text>();
        bones = GameObject.Find("BoneCount").GetComponent<Text>();
    }
    void Update()
    {
        distance.text = Mathf.FloorToInt(player.distance) + "";
        bones.text = player.bones + "";
        highScore.text = Mathf.FloorToInt(player.highScore) + "";
    }
}
