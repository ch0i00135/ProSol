using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    float moveSpeed = 5f;

    public float patrolAreaSizeX;
    public float patrolAreaSizeZ;
    public float patrolPosX;
    public float patrolPosZ;

    public Camera Camera;
    private GameObject player;

    float elapsedTime;
    float rY;
    bool exited = false;
    int lastIdx = 0;
    public Vector3 targetPos;
    Vector3 lastPos;
    public Vector3[] patrol = new Vector3[4];
    void Start()
    {
        player = GameObject.Find("Player");
        patrol[0] = new Vector3(patrolPosX + -patrolAreaSizeX, 1, patrolPosZ + -patrolAreaSizeZ);
        patrol[1] = new Vector3(patrolPosX + -patrolAreaSizeX, 1, patrolPosZ + patrolAreaSizeZ);
        patrol[2] = new Vector3(patrolPosX + patrolAreaSizeX, 1, patrolPosZ + patrolAreaSizeZ);
        patrol[3] = new Vector3(patrolPosX + patrolAreaSizeX, 1, patrolPosZ + -patrolAreaSizeZ);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        gameObject.transform.position = patrol[0];
        targetPos = patrol[1];
    }

    void Update()
    {
        if (Camera == null)
        {
            Debug.LogError("ī�޶� ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }
        Frustum frustum = new Frustum(Camera);
        if (frustum.IsInsideFrustum(player.transform.position))
        {
            targetPos = player.transform.position;
            Move();
            rY = transform.rotation.eulerAngles.y;
            elapsedTime = 0f;
            exited = true;
        }
        else if (exited)
        {
            elapsedTime += Time.deltaTime;
            float totalTime = 3f;
            if (elapsedTime < totalTime/2) transform.rotation = Quaternion.Euler(0f, rY - 30f, 0f);
            if (elapsedTime > totalTime/2) transform.rotation = Quaternion.Euler(0f, rY + 30f, 0f);
            if (elapsedTime > totalTime) exited = false;
        }
        else
        {
            if (transform.position == targetPos)
            {
                if (targetPos == patrol[0])
                {
                    StartCoroutine(RotateBody(1, 0f, 0.3f));
                }
                else if (targetPos == patrol[1])
                {
                    StartCoroutine(RotateBody(2, 90f, 0.3f));
                }
                else if (targetPos == patrol[2])
                {
                    StartCoroutine(RotateBody(3, 180f, 0.3f));
                }
                else if (targetPos == patrol[3])
                {
                    StartCoroutine(RotateBody(0, 270f, 0.3f));
                }
            }
            targetPos = patrol[lastIdx];
            Move();
        }
    }
    private void Move()
    {
        Vector3 direction = targetPos - transform.position;
        direction.y = 0f; // ���� �������θ� ���ϵ��� y�� �� 0���� ����
        direction.Normalize();

        // �ֺ��� ��ֹ��� �ִ��� Ȯ��
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 2f))
        {
            // ��ֹ��� ������ ���� ȸ���ؼ� ���ϵ��� ��
            if (hit.collider.CompareTag("Wall"))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 10, transform.rotation.eulerAngles.z);
                
            }
        }

        // ��ֹ��� ���� ���� ������ �̵� ����
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    IEnumerator RotateBody(int i, float targetAngle, float time)
    {
        float elapsedTime = 0f;
        float totalTime = time;

        Quaternion startRotation = transform.rotation; // ���� ȸ����
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f); // ��ǥ ȸ����

        while (elapsedTime < totalTime)
        {
            // �ð��� ���� ȸ���� ����
            float t = elapsedTime / totalTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lastIdx = i;
    }
    private void OnDrawGizmos()
    {
        patrol[0] = new Vector3(patrolPosX + -patrolAreaSizeX, 1, patrolPosZ + -patrolAreaSizeZ);
        patrol[1] = new Vector3(patrolPosX + -patrolAreaSizeX, 1, patrolPosZ + patrolAreaSizeZ);
        patrol[2] = new Vector3(patrolPosX + patrolAreaSizeX, 1, patrolPosZ + patrolAreaSizeZ);
        patrol[3] = new Vector3(patrolPosX + patrolAreaSizeX, 1, patrolPosZ + -patrolAreaSizeZ);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(patrol[0], patrol[1]);
        Gizmos.DrawLine(patrol[1], patrol[2]);
        Gizmos.DrawLine(patrol[2], patrol[3]);
        Gizmos.DrawLine(patrol[3], patrol[0]);
    }
}

public class Frustum
{
    private readonly Plane[] planes;

    public Frustum(Camera camera)
    {
        planes = GeometryUtility.CalculateFrustumPlanes(camera);
    }

    public bool IsInsideFrustum(Vector3 point)
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
