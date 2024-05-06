using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    public Camera Camera;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Camera == null)
        {
            Debug.LogError("ī�޶� ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // ī�޶��� ���������� ��������
        Frustum frustum = new Frustum(Camera);
        if (frustum.IsInsideFrustum(player.transform.position)) 
        {
            Debug.Log("�߰�");
        }
    }
}

// �������� �÷��� Ŭ����
//public class Frustum
//{
//    private readonly Plane[] planes;

//    public Frustum(Camera camera)
//    {
//        planes = GeometryUtility.CalculateFrustumPlanes(camera);
//    }

//    public bool IsInsideFrustum(Vector3 point)
//    {
//        foreach (var plane in planes)
//        {
//            if (!plane.GetSide(point))
//            {
//                return false;
//            }            
//        }
//        return true;
//    }
//}