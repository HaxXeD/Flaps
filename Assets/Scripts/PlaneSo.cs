using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Sriptable Objects/New Plane", order = 1)]
public class PlaneSo : ScriptableObject
{

    public string planeTitle;
    public Sprite planeImage;
    public int planeCost;
    public bool isPurchased;
    public bool isSelected;
    public GameObject planeObject;
}
