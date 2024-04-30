using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public GameObject head;

    float moveSpeed = 15f;
    float worldAngle = -45f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {

        }
        if (Input.GetKeyDown(KeyCode.P))
        {

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
        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //}
    }
    void rotateHead(float dir)
    {
        head.transform.rotation = Quaternion.Euler(0f, dir + worldAngle, 0f);
    }
    void rotateCamera(float dir)
    {

    }
}
