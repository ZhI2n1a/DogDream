using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 4, player.transform.position.y, transform.position.z);
    }
}
