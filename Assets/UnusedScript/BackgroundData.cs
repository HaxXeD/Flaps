[System.Serializable]

public class BackgroundData
{
    public bool[] isPurchased;
    public bool[] isSelected;
    public BackgroundData(BackgroundSO[] backgrounds){
        isPurchased = new bool[backgrounds.Length];
        isSelected = new bool[backgrounds.Length];
        for(int i = 0;i<backgrounds.Length;i++){
            isPurchased[i] = backgrounds[i].isPurchased;
            isSelected[i] = backgrounds[i].isSelected;
        }
    }
}
