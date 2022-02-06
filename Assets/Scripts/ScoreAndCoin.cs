using UnityEngine;
using TMPro;

public class ScoreAndCoin: MonoBehaviour
{
    [SerializeField] TMP_Text Score;
    [SerializeField] TMP_Text Coin;
    bool isGameOver;
    int score = 0;
    int highScore = 0;
    int coin = 1;
    int totalCoin = 0;


    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        totalCoin = PlayerPrefs.GetInt("totalcoin", 0);
        FindObjectOfType<lowerCollider>().OnPlayerDeath += OnGameOver;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            score = CurrentScore();
            Score.text = score.ToString();
            HighScore();
        }
    }

    public int ReturnCoin()
    {
        return coin - 1;
    }

    public int CurrentScore()
    {
        return (int)(Time.timeSinceLevelLoad * 2f);      
    }

    public int HighScore()
    {
        if (highScore < CurrentScore())
            PlayerPrefs.SetInt("highscore", CurrentScore());
        print(PlayerPrefs.GetInt("highscore"));
        return PlayerPrefs.GetInt("highscore");        
    }
    public int TotalCoins()
    {
        totalCoin += ReturnCoin();
        PlayerPrefs.SetInt("totalcoin", totalCoin);
        return PlayerPrefs.GetInt("totalcoin");
    }
    public void AddCoin()
    {
        Coin.text = coin.ToString();
        coin++;
    }

    private void OnGameOver()
    {
        isGameOver = true;
    }

}
