using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    CoinMonitor coin;
    // public TMP_Text coinTxt;
    public BackgroundSO[] backgroundSOs;
    public GameObject[] backgroundObjects;
    public GameObject[] purchaseBtn;
    public GameObject[] selectBtn;
    public GameObject[] selectedTxt;
    int totalcoin;

    // Start is called before the first frame update
    void Start()
    {
        coin = FindObjectOfType<CoinMonitor>();
        for(int i = 0;i<backgroundSOs.Length;i++){
            backgroundObjects[i].SetActive(true);
        }
        LoadPanel();
        CheckPurchaseable();
        IsPurchased();
        IsSelected();
    }

    // public void AddCoin(){
    //     totalCoins += 100;
    //     coinTxt.text = "Coins: " + totalCoins.ToString();
    //     CheckPurchaseable();
    // }

    public void LoadPanel(){
        for(int i = 0;i<backgroundSOs.Length;i++){
            backgroundObjects[i].GetComponent<BackgroundTemplate>().backgroundName.text = backgroundSOs[i].backgroundName;
            backgroundObjects[i].GetComponent<BackgroundTemplate>().backgroundThumbnail.sprite = backgroundSOs[i].backgroundThubnail;
            backgroundObjects[i].GetComponent<BackgroundTemplate>().backgroundCost.text = backgroundSOs[i].backgroundCost.ToString();
        }
    }

    public void CheckPurchaseable(){
        for(int i = 0;i<backgroundSOs.Length;i++){
            if(coin.TotalCoin>=backgroundSOs[i].backgroundCost)
                purchaseBtn[i].GetComponent<Button>().interactable = true;
            else
                purchaseBtn[i].GetComponent<Button>().interactable = false;
        }
    }

    public void IsPurchased(){
        for(int i = 0;i<backgroundSOs.Length;i++){
            if(backgroundSOs[i].isPurchased)
            {
                purchaseBtn[i].SetActive(false);
                selectBtn[i].SetActive(true);
            }
        }
    }

    public void PurchaseBackground(int btnNumb){
        if(coin.TotalCoin>=backgroundSOs[btnNumb].backgroundCost){
            coin.TotalCoin -= backgroundSOs[btnNumb].backgroundCost;
            coin.coinText.Invoke();
            purchaseBtn[btnNumb].SetActive(false);
            selectBtn[btnNumb].SetActive(true);
            backgroundSOs[btnNumb].isPurchased = true;
        }
        CheckPurchaseable();
    }

    public void SelectBackground(int btnNumb){

        for(int i = 0;i<backgroundSOs.Length;i++){
            if(btnNumb != i){
                backgroundSOs[i].isSelected = false;
                if(backgroundSOs[i].isPurchased){
                    selectBtn[i].SetActive(true);
                }
                selectedTxt[i].SetActive(false);
            }
            else {
                backgroundSOs[btnNumb].isSelected = true;
                selectBtn[btnNumb].SetActive(false);
                selectedTxt[btnNumb].SetActive(true);
            }
        }

    }

        void IsSelected(){
        for(int i = 0;i<backgroundSOs.Length;i++){
            if(backgroundSOs[i].isSelected){
                selectBtn[i].SetActive(false);
                selectedTxt[i].SetActive(true);
            }
            else{
                if(backgroundSOs[i].isPurchased){
                    selectBtn[i].SetActive(true);
                    selectedTxt[i].SetActive(false);
                }
            }
        }
    }
}
