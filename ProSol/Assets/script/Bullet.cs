using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Translate(10*Time.deltaTime,0,0);
    }
}
