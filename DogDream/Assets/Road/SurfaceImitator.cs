/*Please do support www.bitshiftprogrammer.com by joining the facebook page : fb.com/BitshiftProgrammer
Legal Stuff:
This code is free to use no restrictions but attribution would be appreciated.
Any damage caused either partly or completly due to usage of this stuff is not my responsibility*/
using UnityEngine;

public class SurfaceImitator : MonoBehaviour 
{
    public SurfaceGenerator reference;
    public bool generateCollider = true;

    private Mesh mesh;
    private Vector3[] vertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    void LateUpdate()
    {
        vertices = mesh.vertices;
        int counter = 0;
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {                    
                vertices[counter].y = reference.vertices[i].z;
                counter++;
            }
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        if (generateCollider)
        {
            Destroy(GetComponent<MeshCollider>());
            MeshCollider colliderComponent = gameObject.AddComponent<MeshCollider>();
            colliderComponent.sharedMesh = null;
            colliderComponent.sharedMesh = mesh;
        }
    }
}
