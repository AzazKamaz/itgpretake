using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Value { get; private set; } 
    public bool Active { get; private set; }

    private void Start()
    {
        Active = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var pos = Input.mousePosition.x / Screen.width - 0.5f;
            Value = Mathf.Clamp(pos * 3, -1, 1);
            Active = true;
        }

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var pos = touch.position.x / Screen.width - 0.5f;
            Value = Mathf.Clamp(pos * 3, -1, 1);
            Active = true;
        }
    }
}
