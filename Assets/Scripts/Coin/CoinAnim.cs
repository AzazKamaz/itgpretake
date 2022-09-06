using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnim : MonoBehaviour
{
    public float yRate = 0.5f; 
    public float sinRate = 1.5f;
    public float rotRate = 90;

    void Update()
    {
        var t = Time.time;
        transform.localPosition = new Vector3(0, (float) Math.Sin(t * sinRate) * yRate, 0);
        transform.localRotation = Quaternion.Euler(0, t * rotRate, 0);
    }
}