using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject ground;
    public Transform genPoint;

    private float platWidth;
    public float range;

    private BoneGenerator boneGenerator;

    public float boneSpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        platWidth = ground.GetComponent<BoxCollider2D>().size.x;
        boneGenerator = FindObjectOfType<BoneGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < genPoint.position.x)
        {
            if (Random.Range(1f, 100f) < boneSpawnRate)
            {
                boneGenerator.SpawnBone(new Vector3(transform.position.x,
                    transform.position.y + 20f, transform.position.z));
            }

            transform.position = new Vector3(transform.position.x + platWidth + range,
                transform.position.y, transform.position.z);

            //Instantiate(ground, transform.position, transform.rotation);
        }
    }
}
