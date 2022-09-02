using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBorder : MonoBehaviour
{
    Vector2 screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    // Start is called before the first frame update
    void Start()
    {
        print(Camera.main.orthographic);
        print(Camera.main.aspect);
        // Vector2 xCordinates  = new Vector2(-screenHalfSizeWorldUnits.x,screenHalfSizeWorldUnits.x);
        // print(xCordinates);
    }

}
