using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool _alive = true;

    public bool IsAlive => _alive;

    public void Kill()
    {
        _alive = false;
    }
}