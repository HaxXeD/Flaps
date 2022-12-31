using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]protected GameObject _SpawnPowerUp;
    [SerializeField]protected Vector2 _SpawnWaitTime;
    private float _spawnNextTime;
    private Vector2 _screenHalfSizeWorldUnits;

    Vector2 screenHalfSizeWorldUnits;

    [SerializeField] protected float _FirstTimeSpawnWaitTime;
    void Start()
    {
        _spawnNextTime = _SpawnWaitTime.y;
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        StartCoroutine(PowerUp());
    }
    protected virtual IEnumerator PowerUp()
    {
        yield return new WaitForSeconds(_spawnNextTime);
        _spawnNextTime = Mathf.Lerp(_SpawnWaitTime.y, _SpawnWaitTime.x, difficulty.getDifficultyPercent());
        Instanciate(_SpawnPowerUp);
        StartCoroutine(PowerUp());
    }

    private void Instanciate(GameObject name)
    {
        Vector2 spawnPoints = new Vector2(screenHalfSizeWorldUnits.x + 1f, Random.Range(-screenHalfSizeWorldUnits.y + .5f, screenHalfSizeWorldUnits.y - .5f));
        Instantiate(name, spawnPoints, Quaternion.identity);
    }
}
