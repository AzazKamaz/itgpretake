using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretA : MonoBehaviour
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
        var bullet = factory.Instantiate("Bullet");
        bullet.transform.position = transform.position - transform.up;
    }
}
