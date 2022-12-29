using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneGenerator : MonoBehaviour
{
    public ObjectPooler bonePool;
    public float range;

    public void SpawnBone(Vector3 startPos)
    {
        GameObject bone1 = bonePool.GetPooledObject();
        bone1.transform.position = startPos;
        bone1.SetActive(true);

        GameObject bone2 = bonePool.GetPooledObject();
        bone2.transform.position = new Vector3(startPos.x - range, startPos.y, startPos.z);
        bone2.SetActive(true);

        GameObject bone3 = bonePool.GetPooledObject();
        bone3.transform.position = new Vector3(startPos.x + range, startPos.y, startPos.z);
        bone3.SetActive(true);
    }
}
