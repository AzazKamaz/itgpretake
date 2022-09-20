using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class LevelController : MonoBehaviour
{
    private float _multiplier = 0;
    
    public int levelNum { get; private set; }
    public int coinsBalance { get; private set; }
    public TextMeshProUGUI balanceText;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    public SplineContainer spline { get; private set; }
    public int levelReward = 10;

    private void Awake()
    { 
        spline = GetComponent<SplineContainer>();
    }

    private void Start()
    {
        levelNum = PlayerPrefs.GetInt("LastLevel", 0) + 1;
        coinsBalance = PlayerPrefs.GetInt("CoinsBalance", 0);
        balanceText.text = $"{coinsBalance}";
    }

    public void SetMultiplier(float multiplier)
    {
        if (multiplier > _multiplier) _multiplier = multiplier;
    }

    public void OnEndgame()
    {
        if (_multiplier > 0)
        {
            WinScreen.SetActive(true);
        }
        else
        {
            LoseScreen.SetActive(true);
        }
    }

    public void OnWinNext()
    {
        var coins = Mathf.RoundToInt(_multiplier * levelReward);
        PlayerPrefs.SetInt("LastLevel", levelNum);
        PlayerPrefs.SetInt("CoinsBalance", coinsBalance + coins);
        SceneManager.LoadScene("Scenes/LoaderScene");
    }

    public void OnLoseNext()
    {
        SceneManager.LoadScene("Scenes/LoaderScene");
    }
}
