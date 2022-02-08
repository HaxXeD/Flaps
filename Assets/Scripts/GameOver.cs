using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] TMP_Text _score;
    [SerializeField] TMP_Text _coin;
    [SerializeField] TMP_Text _highScore;
    [SerializeField] TMP_Text _totalCoin;

    ScoreAndCoin score1;
    bool isGameOver;

    private void Awake()
    {
        score1 = FindObjectOfType<ScoreAndCoin>();
        isGameOver = false;
    }

    private void Start()
    {
        FindObjectOfType<lowerCollider>().OnPlayerDeath += OnGameOver;
        _coin.text = "COIN: 0";
    }
    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void OnGameOver()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        _score.text = "SCORE: " + score1.CurrentScore().ToString();
        _coin.text = "COIN: " + score1.ReturnCoin().ToString();
        _highScore.text = "HIGHSCORE: " + score1.HighScore().ToString();
        _totalCoin.text = "TOTAL COIN: " + score1.TotalCoins().ToString();
        GameOverScreen.SetActive(true);
        isGameOver = true;
    }
}
