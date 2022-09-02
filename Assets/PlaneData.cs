[System.Serializable]

public class PlaneData
{
    public bool[] isPurchased;
    public bool[] isSelected;
    public PlaneData(PlaneSo[] planes){
        isPurchased = new bool[planes.Length];
        isSelected = new bool[planes.Length];
        for(int i = 0;i<planes.Length;i++){
            isPurchased[i] = planes[i].isPurchased;
            isSelected[i] = planes[i].isSelected;
        }
    }
}
