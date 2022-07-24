using UnityEngine.UI;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    RawImage _img;
    [SerializeField] Vector2 speedMixMax_Horizontal;
    [SerializeField] Vector2 speedMinMax_Vertical;

    void Start(){
        _img = GetComponent<RawImage>();
    }


    private void Update()
    {
        float _x;
        float _y;
        _x = Mathf.Lerp(speedMixMax_Horizontal.x, speedMixMax_Horizontal.y, difficulty.getDifficultyPercent());
        _y = Mathf.Lerp(speedMinMax_Vertical.x,speedMinMax_Vertical.y, difficulty.getDifficultyPercent());
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
    }
}
