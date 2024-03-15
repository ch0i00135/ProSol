using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.position = (new Vector3 (- 9, 0, 0));
    }
    void Update()
    {
        gameObject.transform.Translate(10*Time.deltaTime,0,0);
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
