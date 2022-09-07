using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretB : MonoBehaviour
{
    private readonly float _interval = 1;
    private float _nextShot;
    
    public Factory factory;
    
    void Start()
    {
        _nextShot = Time.time + _interval;
    }

    void Update()
    {
        if (Time.time < _nextShot) return;
        
        _nextShot += _interval;
        var bullet1 = factory.Instantiate("Bullet");
        var bullet2 = factory.Instantiate("Bullet");
        bullet1.transform.position = transform.position + transform.right;
        bullet2.transform.position = transform.position - transform.right;
    }
}