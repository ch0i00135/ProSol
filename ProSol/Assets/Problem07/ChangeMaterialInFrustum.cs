using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterialInFrustum : MonoBehaviour
{
    public Material material1; // �������� ���ο� �ִ� ������Ʈ�� ����� ��Ƽ����
    public Material material2; // �������� �ۿ� �ִ� ������Ʈ�� ����� ��Ƽ����
    private GameObject[] obj;
    private Camera thisCamera;

    private void Start()
    {
        obj = FindObjectsOfType<GameObject>();
        thisCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (thisCamera == null)
        {
            Debug.LogError("ī�޶� ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // ī�޶��� ���������� ��������
        FrustumPlanes frustum = new FrustumPlanes(thisCamera);

        // ��� ���� �ִ� ��� Renderer ��������
        //Renderer[] renderers = FindObjectsOfType<Renderer>();

        //foreach (Renderer renderer in renderers)
        //{
        //    Vector3 size = renderer.bounds.size;
        //    float diameter = Mathf.Max(size.x, Mathf.Max(size.y, size.z));

        //    // Renderer�� �߽����� �������� ���� �ִ��� Ȯ��
        //    if (frustum.IsInsideFrustum(renderer.bounds.center, diameter/2.0f))
        //    {
        //        // �������� ���� �ִ� ��� Material1 ����
        //        renderer.material = material1;
        //    }
        //    else
        //    {
        //        // �������� �ۿ� �ִ� ��� Material2 ����
        //        renderer.material = material2;
        //    }
        //}
        foreach (GameObject go in obj)
        {
            if (go.name == "Sphere(Clone)")
            {
                Vector3 size = go.transform.localScale;
                float diameter = Mathf.Max(size.x, Mathf.Max(size.y, size.z));
            
                if (frustum.IsInsideFrustum(go.transform.position, diameter / 2.0f))
                {
                    go.SetActive(true);
                }
                else
                {
                    go.SetActive(false);
                }
            }            
        }
    }
}

// �������� �÷��� Ŭ����
public class FrustumPlanes
{
    private readonly Plane[] planes;

    public FrustumPlanes(Camera camera)
    {
        planes = GeometryUtility.CalculateFrustumPlanes(camera);
    }

    public bool IsInsideFrustum(Vector3 point, float sideSize)
    {
        foreach (var plane in planes)
        {
            if (!plane.GetSide(point))
            {
                return false;
            }            
        }
        return true;
    }
}