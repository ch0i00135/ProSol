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
            Debug.LogError("카메라 컴포넌트를 찾을 수 없습니다.");
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
        direction.y = 0f; // 수평 방향으로만 향하도록 y축 값 0으로 설정
        direction.Normalize();

        // 주변에 장애물이 있는지 확인
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 2f))
        {
            // 장애물을 만났을 때는 회전해서 피하도록 함
            if (hit.collider.CompareTag("Wall"))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 10, transform.rotation.eulerAngles.z);
                
            }
        }

        // 장애물이 없을 때는 보통의 이동 수행
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    IEnumerator RotateBody(int i, float targetAngle, float time)
    {
        float elapsedTime = 0f;
        float totalTime = time;

        Quaternion startRotation = transform.rotation; // 현재 회전값
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f); // 목표 회전값

        while (elapsedTime < totalTime)
        {
            // 시간에 따라 회전값 보간
            float t = elapsedTime / totalTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            // 시간 업데이트
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
