using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 90f;
    public float cameraHeightOffset = 10f;
    public float cameraRadius = 10f; // 카메라의 원형 경로 반지름

    void Update()
    {
        // O와 P 키 입력으로 카메라 회전
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(RotateCameraAroundCharacter(-90f));
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(RotateCameraAroundCharacter(90f));
        }

        // 키보드 입력을 받아 플레이어 이동
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 키 입력 방향으로 캐릭터 회전
        if (moveDirection != Vector3.zero)
        {
            float cameraRotationY = Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad;
            Vector3 rotatedMoveDirection = new Vector3(
                moveDirection.x * Mathf.Cos(cameraRotationY - Mathf.PI / 4) + moveDirection.z * Mathf.Sin(cameraRotationY - Mathf.PI / 4),
                0f,
                -moveDirection.x * Mathf.Sin(cameraRotationY - Mathf.PI / 4) + moveDirection.z * Mathf.Cos(cameraRotationY - Mathf.PI / 4)
            ).normalized;

            Quaternion toRotation = Quaternion.LookRotation(rotatedMoveDirection);
            transform.rotation = toRotation;
        }

        // 키 입력이 있는 경우만 캐릭터를 이동시킵니다.
        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        {
            float cameraRotationY = Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad;
            Vector3 correctedMoveDirection = new Vector3(
                moveDirection.x * Mathf.Cos(cameraRotationY) + moveDirection.z * Mathf.Sin(cameraRotationY),
                0f,
                -moveDirection.x * Mathf.Sin(cameraRotationY) + moveDirection.z * Mathf.Cos(cameraRotationY)
            ).normalized;

            Vector3 newPosition = transform.position + correctedMoveDirection * moveSpeed * Time.deltaTime;
            transform.position = newPosition;
        }

        Camera.main.transform.LookAt(transform.position);
    }

    // 캐릭터를 중심으로 카메라를 원형 경로에 따라 회전시키는 함수
    IEnumerator RotateCameraAroundCharacter(float angle)
    {
        Vector3 playerPosition = transform.position;
        Vector3 cameraPosition = Camera.main.transform.position;

        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 newCameraPosition = playerPosition + rotation * (cameraPosition - playerPosition).normalized * cameraRadius;

        Quaternion startRotation = Camera.main.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(playerPosition - newCameraPosition);

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Camera.main.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime / 1f;
            yield return null;
        }

        // 회전 애니메이션이 완료된 후 카메라 위치를 조정
        //Camera.main.transform.position = newCameraPosition;
    }
}
