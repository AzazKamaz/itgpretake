using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float _nextShoot;

    public Transform firePoint;
    public float fov = 90;
    public float cooldown = 1;
    public float speed = 10;
    public GameObject projectilePrefab;

    private void Start()
    {
        _nextShoot = Time.time + cooldown;
    }

    private void Update()
    {
        var all = GameObject.FindGameObjectsWithTag("Player");

        // Draw lines to all possible targets for debug
        foreach (var i in all)
        {
            var dir = (i.transform.position - firePoint.position).normalized;
            var ang = Vector3.Angle(dir, firePoint.forward);

            if (ang <= fov / 2)
            {
                Debug.DrawLine(firePoint.position, i.transform.position, Color.red);
            }
            else
            {
                Debug.DrawLine(firePoint.position, i.transform.position, Color.blue);
            }

        }
        
        if (Time.time < _nextShoot) return;

        foreach (var i in all)
        {
            var dir = (i.transform.position - firePoint.position).normalized;
            var ang = Vector3.Angle(dir, firePoint.forward);
            
            if (!(ang <= fov / 2)) continue;


            var proj = GameObject.Instantiate(projectilePrefab);
            proj.transform.position = firePoint.position;
            proj.GetComponent<Rigidbody>().velocity = dir * speed;

            _nextShoot = Time.time + cooldown;
            break;
        }
    }

    private void OnDrawGizmos()
    {
        var position = firePoint.position;
        var forward = firePoint.forward;
        var up = firePoint.up;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(position, forward * 5);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(position, Quaternion.AngleAxis(fov / 2, up) * forward * 5);
        Gizmos.DrawRay(position, Quaternion.AngleAxis(-fov / 2, up) * forward * 5);
    }
}