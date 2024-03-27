using System.Collections;
using DataStrucuture;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bullet;
    Queue<GameObject> bullets = new Queue<GameObject>();
    int InstantiateCount;
    void Start()
    {
        InstantiateCount = 10;
        for (int i = 0; i < InstantiateCount; i++)
        {
            bullets.Enqueue(bullet);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject temp = bullets.Dequeue();
            if (InstantiateCount > 0)
            {
                Instantiate(temp);
                InstantiateCount--;
            }
            else
            {
                temp.gameObject.transform.position = (new Vector3(-9, 0, 0));
                temp.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        bullets.Enqueue(other.gameObject);
    }
}
