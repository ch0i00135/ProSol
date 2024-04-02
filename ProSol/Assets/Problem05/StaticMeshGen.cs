using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

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
        if (GUILayout.Button("Remove Mesh"))
        {
            script.Remove();
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
            new Vector3(1.0f, 2.0f, 0.0f),
            new Vector3(1.25f, 1.25f, 0.0f),
            new Vector3(2.0f, 1.22f, 0.0f),
            new Vector3(1.4f, 0.75f, 0.0f),
            new Vector3(1.7f, 0.0f, 0.0f),
            new Vector3(1.0f, 0.45f, 0.0f),
            new Vector3(0.4f, 0.0f, 0.0f),
            new Vector3(0.6f, 0.8f, 0.0f),
            new Vector3(0.0f, 1.22f, 0.0f),
            new Vector3(0.75f, 1.25f, 0.0f),
            
            new Vector3(1.0f, 2.0f, 5.0f),
            new Vector3(1.25f, 1.25f, 5.0f),
            new Vector3(2.0f, 1.22f, 5.0f),
            new Vector3(1.4f, 0.75f, 5.0f),
            new Vector3(1.7f, 0.0f, 5.0f),
            new Vector3(1.0f, 0.45f, 5.0f),
            new Vector3(0.4f, 0.0f, 5.0f),
            new Vector3(0.6f, 0.8f, 5.0f),
            new Vector3(0.0f, 1.22f, 5.0f),
            new Vector3(0.75f, 1.25f, 5.0f),
        };

        mesh.vertices = vertices;

        int[] triangleIndices = new int[]
        {
            //앞
            0,1,9,
            1,2,3,
            3,4,5,
            5,6,7,
            7,8,9,
            1,3,5,
            5,7,9,
            1,5,9,
            //뒤
            19, 11, 10,
            13, 12, 11,
            15, 14, 13,
            17, 16, 15,
            19, 18, 17,
            15, 13, 11,
            19, 17, 15,
            19, 15, 11,
            //기둥
            0,10,1,
            10,11,1,
            1,11,2,
            11,12,2,
            2,12,3,
            12,13,3,
            3,13,4,
            13,14,4,
            4,14,5,
            14,15,5,
            5,15,6,
            15,16,6,
            6,16,7,
            16,17,7,
            7,17,8,
            17,18,8,
            8,18,9,
            18,19,9,
            9,19,0,
            19,10,0,
        };

        mesh.triangles = triangleIndices;

        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh;

        // Shader 설정
        Material material = new Material(Shader.Find("Standard"));
        material.color = Color.yellow;
        mr.material = material;
    }

    public void Remove()
    {
        DestroyImmediate(this.GetComponent<MeshFilter>());
        DestroyImmediate(this.GetComponent<MeshRenderer>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
