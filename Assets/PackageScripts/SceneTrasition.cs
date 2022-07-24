using UnityEngine;

public class SceneTrasition : MonoBehaviour
{
    [SerializeField] GameObject trasitionImage;
    // Start is called before the first frame update
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        GetCamera();
    }
    private void Update()
    {
        if (canvas.worldCamera == null)
        {
            GetCamera();
        }
    }
    public void GetCamera()
    {
        canvas.worldCamera = Camera.main;
    }

    public GameObject GetTrasitionImage(){
        return trasitionImage;
    }
}
