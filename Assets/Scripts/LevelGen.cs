using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGen : MonoBehaviour
{
    public GameObject winTrigger;
    public GameObject floorPrefab;
    public GameObject coinPrefab;

    void Start()
    {
        var last = Vector3.zero;
        for (int i = 0; i < 11; i++)
        {
            var floor = GameObject.Instantiate(floorPrefab);
            var len = Random.Range(10, 20);
            if (i % 2 == 0)
            {
                floor.transform.position = last + Vector3.forward * len / 2;
                last += Vector3.forward * len;
                floor.transform.localScale = new Vector3(1, 1, len);
            }
            else
            {
                floor.transform.position = last + Vector3.right * len / 2;
                last += Vector3.right * len;
                floor.transform.localScale = new Vector3(len, 1, 1);
            }
            
            var corner = GameObject.Instantiate(floorPrefab);
            corner.transform.position = last;

            if (i % 2 == 1)
            {
                var coin = GameObject.Instantiate(coinPrefab);
                coin.transform.position = last;
            }
        }

        winTrigger.transform.position = last;
    }
}