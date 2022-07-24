using UnityEngine;


public class ObjectMovementScript : MonoBehaviour
{
    enum MovementType
    {
        EnableWave,
        DisableWave
    }

    [SerializeField] MovementType movementType;
    Vector2 pos;
    [SerializeField] Vector2 speedMinMax;
    float visibility;
    //for Wave
    [SerializeField] Vector2 maxAmplitude;
    //float currentAmplitude;
    //float amplitude;
    float sinCenterY;


    private void Start()
    {
        //amplitude = Random.Range(1,currentAmplitude);
        sinCenterY = transform.position.y;
        visibility = -Camera.main.aspect * Camera.main.orthographicSize - transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        switch (movementType)
        {
            case MovementType.DisableWave:
                Speed();
                break;

            case MovementType.EnableWave:
                Speed();
                Amplitude();
                break;
        }

        transform.position = pos;
        if (transform.position.x < visibility)
        {

            Destroy(gameObject);
        }
    }

    private void Amplitude()
    {
        float currentAmplitude = Mathf.Lerp(maxAmplitude.x, maxAmplitude.y, difficulty.getDifficultyPercent());
        float sin = Mathf.Sin(pos.x)*currentAmplitude;
        pos.y = sin + sinCenterY;
    }

    private void Speed()
    {
        float speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, difficulty.getDifficultyPercent());
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        pos = transform.position;
    }
}
