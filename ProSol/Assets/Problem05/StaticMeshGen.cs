using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]
public class StaticMeshGenEditor : Editor
{
    //버튼만들기 예제
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGen script = (StaticMeshGen)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.GenerateMesh();
        }

    }
}

//메쉬만들기 예제
public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3 (1.0f, 0.0f, 0.0f),
            new Vector3 (0.75f, 0.75f, 0.0f),
            new Vector3 (0.0f, 0.78f, 0.0f),
            new Vector3 (0.6f, 1.25f, 0.0f),
            new Vector3 (0.3f, 2.0f, 0.0f),
            new Vector3 (1.0f, 1.55f, 0.0f),
            new Vector3 (1.6f, 2.0f, 0.0f),
            new Vector3 (1.4f, 1.2f, 0.0f),
            new Vector3 (2.0f, 0.78f, 0.0f),
            new Vector3 (1.25f, 0.75f, 0.0f),
        };

        mesh.vertices = vertices;

        int[] triangleIndices = new int[]
        {
            0,1,9,
            1,2,3,
            3,4,5,
            5,6,7,
            7,8,9,
            1,3,5,
            5,7,9,
            1,5,9,
        };

        mesh.triangles = triangleIndices;

        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
