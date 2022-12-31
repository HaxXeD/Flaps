using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Sriptable Objects/New PowerUp", order = 1)]
public class PowerUpSO : ScriptableObject
{
    public GameObject powerUP;
    public string powerUpName;
    public Sprite powerUpImage;
    public int powerUpCost;
    public  bool isPurchased;
}
