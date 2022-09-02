[System.Serializable]

public class CollectableData
{
    public bool[] isPurchased;
    public CollectableData(PowerUpSO[] powerUps){
        isPurchased = new bool[powerUps.Length];
        for(int i = 0;i<powerUps.Length;i++){
            isPurchased[i] = powerUps[i].isPurchased;
        }
    }
}