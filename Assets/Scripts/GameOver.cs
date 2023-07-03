using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    SettingButtons settingButtons;
    UIManager uIManager;
    ScoreAndCoin scoreAndCoin;
    GameObject Score_Coin;
    private void Awake()
    {
        scoreAndCoin = FindObjectOfType<ScoreAndCoin>();
    }

    private void Start()
    {
        settingButtons = FindObjectOfType<SettingButtons>();
        uIManager = FindObjectOfType<UIManager>();
        FindObjectOfType<lowerCollider>().OnPlayerDeath += OnGameOver;
        Score_Coin = uIManager.GetGameOverUI().transform.GetChild(1).gameObject;
    }


    public void OnGameOver()
    {
        print("gameOver");

        settingButtons.ShowGameOverUI();
        GameObject HighSC = uIManager.GetGameOverUI().transform.GetChild(1).gameObject;
        settingButtons.SetCurrentScore("SCORE: " + (scoreAndCoin.CurrentScore() + scoreAndCoin.ReturnCoin()).ToString()) ;
        settingButtons.SetHighScore("BEST: " + scoreAndCoin.HighScore().ToString());
        // HighSC.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = "COINS: " + scoreAndCoin.ReturnCoin().ToString();
        scoreAndCoin.TotalCoins();
    }
}
