using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StaticMeshGen2))]
public class StaticMeshGenEditor2 : Editor
{
    //버튼만들기 예제
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGen2 script = (StaticMeshGen2)target;

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
public class StaticMeshGen2 : MonoBehaviour
{
    public Material material;
    public float rot;
    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();
                
        float r1 = 1.0f;  // 외부 반지름
        float r2 = 0.4f;  // 내부 반지름
        float height = 2.0f;  // 기둥의 높이

        Vector3[] vertices = new Vector3[20];  // 상단 10개 + 하단 10개

        // 상단
        for (int i = 0; i < 5; i++)
        {
            float angle = rot+(2 * Mathf.PI * i) / 5;
            vertices[i*2] = new Vector3(Mathf.Cos(angle) * r1, Mathf.Sin(angle) * r1, height);
        }
        for (int i = 0; i < 5; i++)
        {
            float angle = rot + (Mathf.PI / 5) + (2 * Mathf.PI * i) / 5;
            vertices[i*2+1] = new Vector3(Mathf.Cos(angle) * r2, Mathf.Sin(angle) * r2, height);
        }
        //하단
        for (int i = 0; i < 5; i++)
        {
            float angle = rot + (2 * Mathf.PI * i) / 5;
            vertices[i * 2+10] = new Vector3(Mathf.Cos(angle) * r1, Mathf.Sin(angle) * r1, 0);
        }
        for (int i = 0; i < 5; i++)
        {
            float angle = rot + (Mathf.PI / 5) + (2 * Mathf.PI * i) / 5;
            vertices[i * 2 + 11] = new Vector3(Mathf.Cos(angle) * r2, Mathf.Sin(angle) * r2, 0);
        }

        int[] triangleIndices = new int[]
        {
            //앞
            0,1,9,
            1,2,3,
            3,4,5,
            5,6,7,
            7,8,9,
            1,3,5,
            1,5,9,
            5,7,9,
            //뒤
            19, 11, 10,
            13, 12, 11,
            15, 14, 13,
            17, 16, 15,
            19, 18, 17,
            15, 13, 11,
            19, 15, 11,
            19, 17, 15,
            //기둥
            0,10,1,
            1,11,2,
            2,12,3,
            3,13,4,
            4,14,5,
            5,15,6,
            6,16,7,
            7,17,8,
            8,18,9,
            9,19,0,
            10,11,1,
            11,12,2,
            12,13,3,
            13,14,4,
            14,15,5,
            15,16,6,
            16,17,7,
            17,18,8,
            18,19,9,
            19,10,0,
        };
        Vector3[] normals = new Vector3[vertices.Length];

        for (int i = 0; i < triangleIndices.Length; i+=3)
        {
            int i1 = triangleIndices[i];
            int i2 = triangleIndices[i + 1];
            int i3 = triangleIndices[i + 2];

            Vector3 side1 = vertices[i2] - vertices[i1];
            Vector3 side2 = vertices[i3] - vertices[i1];
            Vector3 normal = Vector3.Cross(side1, side2).normalized;

            normals[i1] += normal;
            normals[i2] += normal;
            normals[i3] += normal;
        }
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = normals[i].normalized;
        }
        Debug.Log($"normals length: {normals.Length}, mesh normals length: {mesh.normals.Length}, ");

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangleIndices;
        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh;

        // Shader 설정
        //Material material = new Material(Shader.Find("Unlit/Yellow"));
        //material.color = Color.yellow;
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
