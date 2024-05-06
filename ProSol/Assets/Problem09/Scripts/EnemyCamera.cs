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
            Debug.LogError("카메라 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        // 카메라의 프러스텀을 가져오기
        Frustum frustum = new Frustum(Camera);
        if (frustum.IsInsideFrustum(player.transform.position)) 
        {
            Debug.Log("발견");
        }
    }
}

// 프러스텀 플레인 클래스
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