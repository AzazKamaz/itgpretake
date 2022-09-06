using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int _score = 0;

    public void Collect()
    {
        _score++;
        Debug.Log($"Coin Collected! Score is {_score}");
    }
}
