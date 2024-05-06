using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    int offset = -14;
    public GameObject[] wall;
    List<Dictionary<string, object>> MapDATA;

    void Start()
    {
        MapDATA = CSVReader.Read("Map");
        for (int i = 0; i < MapDATA.Count; i++)
        {
            for (int j = 0; j < MapDATA.Count; j++)
            {
                int w = (int)MapDATA[j][i.ToString()];
                if (w != 0)
                {
                    GameObject wObj = Instantiate(wall[w]);
                    wObj.transform.SetParent(transform, false);
                    wObj.transform.position = new Vector3(offset + j * 2, 0, offset + i * 2);
                }
            }
        }
    }
    
    void Update()
    {
        
    }
}
