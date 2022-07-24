using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "planeModels",menuName ="plane")]
public class PlaneModels : ScriptableObject
{
    public string Name;
    public float topSpeed;
    public float HP;
    public enum Handling{
        easy,
        normal,
        difficult
    }
    public Handling handling;
    public GameObject Plane;

}
