using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSplineMovement : MonoBehaviour
{
    private PlayerInput _input;
    private LevelController _level;
    private float _progress;
    private float _len;

    public float speed = 5f;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _level = FindObjectOfType<LevelController>();
    }

    private void Start()
    {
        _len = _level.spline.CalculateLength();
        _progress = 0;
        UpdatePosition();
    }

    private void FixedUpdate()
    {
        if (_input.Active)
        {
            _progress += Time.deltaTime * speed / _len;
            
            // Approximately compensate speed when spline is not horizontal. The case of endgame ladder
            var tangent = (Vector3)_level.spline.EvaluateTangent(_progress);
            if (tangent.magnitude > 0)
            {
                var angle = Mathf.Acos(Vector3.Dot(Vector3.up, tangent.normalized));
                _progress += (1f / Mathf.Sin(angle) - 1) * Time.deltaTime * speed / _len;
            }
            
            UpdatePosition();
        }
    }

    private bool UpdatePosition()
    {
        if (_progress > 1) return false;
        if (!_level.spline.Evaluate(_progress, out var pos, out var fwd, out var up)) return false;

        transform.position = pos;
        
        var fwdVec = (Vector3) fwd;
        if (fwdVec.sqrMagnitude > 0)
        {
            var fwdHor = Vector3.ProjectOnPlane(fwdVec, Vector3.up); // Update forward to be horizontal
            var rot = Quaternion.LookRotation(fwdHor, Vector3.up);
            transform.rotation = rot;
        }

        return true;
    }
}
