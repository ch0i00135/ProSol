using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 90f;
    public float cameraHeightOffset = 10f;
    public float cameraRadius = 10f; // ī�޶��� ���� ��� ������

    void Update()
    {
        // O�� P Ű �Է����� ī�޶� ȸ��
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(RotateCameraAroundCharacter(-90f));
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(RotateCameraAroundCharacter(90f));
        }

        // Ű���� �Է��� �޾� �÷��̾� �̵�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Ű �Է� �������� ĳ���� ȸ��
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

        // Ű �Է��� �ִ� ��츸 ĳ���͸� �̵���ŵ�ϴ�.
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

    // ĳ���͸� �߽����� ī�޶� ���� ��ο� ���� ȸ����Ű�� �Լ�
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

        // ȸ�� �ִϸ��̼��� �Ϸ�� �� ī�޶� ��ġ�� ����
        //Camera.main.transform.position = newCameraPosition;
    }
}
