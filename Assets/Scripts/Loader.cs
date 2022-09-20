using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public List<string> levels = new List<string>();
    public int tutorialCount = 2;

    private void Start()
    {
        var level = PlayerPrefs.GetInt("LastLevel") + 1;

        if (level >= levels.Count)
        {
            level = (level - tutorialCount) % (levels.Count - tutorialCount) + tutorialCount;
        }

        SceneManager.LoadScene(levels[level]);
    }
}
