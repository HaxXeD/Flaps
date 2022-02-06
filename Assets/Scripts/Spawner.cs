using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Fuel")]
    [SerializeField] GameObject fuel;
    [SerializeField] Vector2 spawnWaitTimeFuel;

    [Header("Coin")]
    [SerializeField] GameObject coin;
    [SerializeField] Vector2 spawnWaitTimeCoin;

    [Header("Enemy")]
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnWaitTimeEnemy;

    [Header("Enemy")]
    [SerializeField] GameObject powerUp;
    [SerializeField] Vector2 spawnWaitTimePowerUp;

    float spawnNextTimeFuel;
    float spawnNextTimeCoin;
    float spawnNextTimeEnemy;
    float spawnNextTimePowerUp;

    Vector2 screenHalfSizeWorldUnits;

    // Start is called before the first frame update
    void Start()
    {
        spawnNextTimeFuel = spawnWaitTimeFuel.y;
        spawnNextTimeCoin = spawnWaitTimeCoin.y;
        spawnNextTimeEnemy = spawnWaitTimeEnemy.y;
        spawnNextTimePowerUp = spawnWaitTimePowerUp.y;
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        Fuel();
        Coin();
        Enemy();
        PowerUp();
    }

    private void PowerUp()
    {
        if (Time.time > spawnNextTimePowerUp)
        {
            spawnNextTimePowerUp = Time.time + Mathf.Lerp(spawnWaitTimePowerUp.y, spawnWaitTimePowerUp.x, difficulty.getDifficultyPercent());
            Instanciate(powerUp);
        }
    }

    private void Fuel()
    {
        if (Time.time > spawnNextTimeFuel)
        {
            spawnNextTimeFuel = Time.time + Mathf.Lerp(spawnWaitTimeFuel.y, spawnWaitTimeFuel.x, difficulty.getDifficultyPercent());
            Instanciate(fuel);
        }
    }

    private void Instanciate(GameObject name)
    {
        Vector2 spawnPoints = new Vector2(screenHalfSizeWorldUnits.x + .5f, Random.Range(-screenHalfSizeWorldUnits.y + .5f, screenHalfSizeWorldUnits.y - .5f));
        Instantiate(name, spawnPoints, Quaternion.identity);
    }

    private void Coin()
    {
        if (Time.time > spawnNextTimeCoin)
        {
            spawnNextTimeCoin = Time.time + Mathf.Lerp(spawnWaitTimeCoin.y, spawnWaitTimeCoin.x, difficulty.getDifficultyPercent());
            Instanciate(coin);
        }
    }

    private void Enemy()
    {
        if (Time.time > spawnNextTimeEnemy)
        {
            spawnNextTimeEnemy = Time.time + Mathf.Lerp(spawnWaitTimeEnemy.y, spawnWaitTimeEnemy.x, difficulty.getDifficultyPercent());
            Instanciate(enemy);
        }
    }
}
