using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class gen : MonoBehaviour
{
    //1227365
    public int Gen(int num)
    {
        int sum=0;
        int n, nn, nnn, nnnn;
        nnnn = num / 1000;
        nnn = (num-nnnn*1000) / 100;
        nn = (num - nnnn * 1000 - nnn*100) / 10;
        n = (num - nnnn * 1000 - nnn * 100 - nn * 10);
        sum = num + nnnn + nnn  + nn + n;

        Debug.Log("num="+num+" //// "+nnnn+" /// "+nnn+" // "+nn+" / "+n+" s="+sum);

        return sum;
    }
    private void Start()
    {
        List<int> arr=new List<int>();
        int sum = 0;
        for(int i = 0; i <= 5000; i++)
        {
            arr.Add(i);
        }
        for (int i = 1; i <= 5000; i++)
        {
            if(Gen(i)<=5000)arr[Gen(i)] = 0;
        }
        //for(int i = 0; i < arr.Count; i++)
        //{
        //    sum += arr[i];
        //}
        sum = arr.Sum();
        Debug.Log("´ä: 1227365");
        Debug.Log(sum);

    }
}
