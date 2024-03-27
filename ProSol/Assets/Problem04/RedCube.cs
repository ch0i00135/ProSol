using DataStrucuture;
using UnityEngine;

public class RedCube : MonoBehaviour
{
    public GameObject bullet;
    Stack<GameObject> bullets = new Stack<GameObject>();
    int InstantiateCount;
    void Start()
    {
        InstantiateCount = 10;
        for (int i = 0; i < InstantiateCount; i++)
        {
            bullets.Push(bullet);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject temp = bullets.Pop();
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
        bullets.Push(other.gameObject);
    }
}
