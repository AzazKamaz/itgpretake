using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStack : MonoBehaviour
{
    private AttachTarget _target;
    
    public GameObject stackedPrefab;
    public Rigidbody character;

    private void Awake()
    {
        _target = GetComponent<AttachTarget>();
    }

    public void AddCube()
    {
        var created = Instantiate(stackedPrefab, character.position, character.rotation);
        created.GetComponent<AttachXZ>().target = _target;
        character.MovePosition(character.position + transform.up);
    }
}
