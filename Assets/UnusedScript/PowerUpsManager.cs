using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerUpsManager : MonoBehaviour
{
    CoinMonitor coin;
    public PowerUpSO[] powerUpSOs;
    public GameObject[] powerUpObjects;
    public GameObject[] purchaseBtn;
    // Start is called before the first frame update
    void Start()
    {
        coin = FindObjectOfType<CoinMonitor>();
        for(int i = 0;i<powerUpSOs.Length;i++){
            powerUpObjects[i].SetActive(true);
        }

        LoadPanel();
        CheckPurchaseable();
        IsPurchased();
    }

    public void LoadPanel(){
        for(int i = 0;i<powerUpSOs.Length;i++){
            powerUpObjects[i].GetComponent<PowerUpsTemplate>().powerUpName.text = powerUpSOs[i].powerUpName;
            powerUpObjects[i].GetComponent<PowerUpsTemplate>().powerUpImage.sprite = powerUpSOs[i].powerUpImage;
            powerUpObjects[i].GetComponent<PowerUpsTemplate>().powerUpCost.text = powerUpSOs[i].powerUpCost.ToString();
        }
    }

    public void CheckPurchaseable(){
        for(int i = 0;i<powerUpSOs.Length;i++){
            if(coin.TotalCoin>=powerUpSOs[i].powerUpCost)
                purchaseBtn[i].GetComponent<Button>().interactable = true;
            else
                purchaseBtn[i].GetComponent<Button>().interactable = false;
        }
    }

    public void IsPurchased(){
        for(int i = 0;i<powerUpSOs.Length;i++){
            if(powerUpSOs[i].isPurchased)
            {
                purchaseBtn[i].SetActive(false);
            }
        }
    }

    
    public void PurchasePowerUp(int btnNumb){
        if(coin.TotalCoin>=powerUpSOs[btnNumb].powerUpCost){
            coin.TotalCoin -= powerUpSOs[btnNumb].powerUpCost;
            coin.coinText.Invoke();
            purchaseBtn[btnNumb].SetActive(false);
            powerUpSOs[btnNumb].isPurchased = true;
        }
        CheckPurchaseable();
    }
    
    // public void AddCoin(){
    //     totalCoins += 100;
    //     coinTxt.text = "Coins: " + totalCoins.ToString();
    //     CheckPurchaseable();
    // }
}
