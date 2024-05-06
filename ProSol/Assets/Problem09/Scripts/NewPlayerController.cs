using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public GameObject head;

    GameObject cameraObj;
    bool rot = false;
    float moveSpeed = 5f;
    public static float worldAngle = -45f;

    void Start()
    {
        cameraObj = GameObject.Find("CameraObj");
    }

    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.O) && !rot)
        {
            StartCoroutine(rotateAngle(90f));
        }
        if (Input.GetKeyDown(KeyCode.P) && !rot)
        {
            StartCoroutine(rotateAngle(-90f));
        }
        if (Input.GetKey(KeyCode.W))
        {
            rotateHead(0f);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.A))
        {
            rotateHead(-90f);
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.S))
        {
            rotateHead(180f);
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.D))
        {
            rotateHead(90f);
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        }
        transform.rotation = Quaternion.Euler(0f, worldAngle, 0f);
        cameraObj.transform.rotation = Quaternion.Euler(0f, worldAngle, 0f);
    }
    void rotateHead(float dir)
    {
        head.transform.rotation = Quaternion.Euler(0f, dir + worldAngle, 0f);
    }

    IEnumerator rotateAngle(float dir)
    {
        rot = true;
        float angle = worldAngle;
        float elapsedTime = 0;
        float totalTime = 1f;
        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            worldAngle = angle + dir * elapsedTime;
            yield return null;
        }
        rot = false;
    }
}
