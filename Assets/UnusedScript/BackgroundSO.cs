using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Sriptable Objects/New Background", order = 1)]
public class BackgroundSO : ScriptableObject
{
    public string backgroundName;
    public Sprite backgroundThubnail;
    public Texture backgroundImage;
    public int backgroundCost;
    public  bool isPurchased;
    public bool isSelected;
}
