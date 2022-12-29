using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoneGenerator boneGenerator;

    public float randomThreshold;

    // Start is called before the first frame update
    void Start()
    {
        boneGenerator = FindObjectOfType<BoneGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1f, 100f) < randomThreshold)
        {
            boneGenerator.SpawnBone(new Vector3(transform.position.x,
                transform.position.y + 1f, transform.position.z));
        }
    }
}
