using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] GameObject transitionImage;
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

    public GameObject GetTransitionImage(){
        return transitionImage;
    }
}
