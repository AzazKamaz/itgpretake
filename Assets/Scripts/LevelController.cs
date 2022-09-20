using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SplineContainer))]
public class LevelController : MonoBehaviour
{
    private float _multiplier = 0;
    private IEnumerator _nextCoroutine = null;
    private bool _nextPressed = false;
    
    public int levelNum { get; private set; }
    public int coinsBalance { get; private set; }
    public TextMeshProUGUI balanceText;

    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject coinTweenPrefab;

    public SplineContainer spline { get; private set; }
    public int levelReward = 10;

    private void Awake()
    { 
        spline = GetComponent<SplineContainer>();
    }

    private void Start()
    {
        levelNum = PlayerPrefs.GetInt("LastLevel", -1) + 1;
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
            winScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }
    }

    public void OnWinNext()
    {
        if (_nextPressed) return;
        _nextPressed = true;

        _nextCoroutine = WinCoroutine();
        StartCoroutine(_nextCoroutine);
    }

    public void OnLoseNext()
    {
        if (_nextPressed) return;
        _nextPressed = true;
        
        _nextCoroutine = LoseCoroutine();
        StartCoroutine(_nextCoroutine);
    }

    private IEnumerator LoseCoroutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scenes/LoaderScene");
    }

    private IEnumerator WinCoroutine()
    {
        var coins = Mathf.RoundToInt(_multiplier * levelReward);
        PlayerPrefs.SetInt("LastLevel", levelNum);
        PlayerPrefs.SetInt("CoinsBalance", coinsBalance + coins);
        
        for (var i = 0; i < coins; i++)
        {
            var pos = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
            var inst = Instantiate(coinTweenPrefab, winScreen.transform);
            inst.GetComponent<RectTransform>().anchoredPosition = pos;
            inst.GetComponent<CoinTween>().onComplete.AddListener(IncrementCoin);
            yield return new WaitForSeconds(0.05f);                      
        }
        
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Scenes/LoaderScene");
    }

    private void IncrementCoin()
    {
        balanceText.text = $"{++coinsBalance}";
    }
}
