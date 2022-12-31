using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlaneManager : MonoBehaviour
{
    CoinMonitor coin;
    public PlaneSo[] planeSO;
    public GameObject[] planeObject;
    public GameObject[] purchaseBtn;
    public GameObject[] selectBtn;
    public GameObject[] selectedTxt;

    // Start is called before the first frame update
    void Start()
    {
        coin = FindObjectOfType<CoinMonitor>();
        // totalCoins = PlayerPrefs.GetInt("totalcoin",0);
        for(int i = 0;i<planeSO.Length;i++){
            planeObject[i].SetActive(true);
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
        for(int i = 0;i<planeSO.Length;i++){
            planeObject[i].GetComponent<PlaneTemplate>().planeTitle.text = planeSO[i].planeTitle;
            planeObject[i].GetComponent<PlaneTemplate>().planeImage.sprite = planeSO[i].planeImage;
            planeObject[i].GetComponent<PlaneTemplate>().planeCost.text = planeSO[i].planeCost.ToString();
        }
    }

    public void CheckPurchaseable(){
        for(int i = 0;i<planeSO.Length;i++){
            if(coin.TotalCoin>=planeSO[i].planeCost)
                purchaseBtn[i].GetComponent<Button>().interactable = true;
            else
                purchaseBtn[i].GetComponent<Button>().interactable = false;
        }
    }

    public void IsPurchased(){
        for(int i = 0;i<planeSO.Length;i++){
            if(planeSO[i].isPurchased)
            {
                purchaseBtn[i].SetActive(false);
                selectBtn[i].SetActive(true);
            }
        }
    }

    public void PurchasePlane(int btnNumb){
        if(coin.TotalCoin>=planeSO[btnNumb].planeCost){
            coin.TotalCoin -= planeSO[btnNumb].planeCost;
            coin.coinText.Invoke();
            purchaseBtn[btnNumb].SetActive(false);
            selectBtn[btnNumb].SetActive(true);
            planeSO[btnNumb].isPurchased = true;
        }
        CheckPurchaseable();
    }

     public void SelectPlane(int btnNumb){

        for(int i = 0;i<planeSO.Length;i++){
            if(btnNumb != i){
                planeSO[i].isSelected = false;
                if(planeSO[i].isPurchased){
                    selectBtn[i].SetActive(true);
                }
                
                selectedTxt[i].SetActive(false);
    
            }
            else {
                planeSO[btnNumb].isSelected = true;
                selectBtn[btnNumb].SetActive(false);
                selectedTxt[btnNumb].SetActive(true);
            }
        }

    }

    void IsSelected(){
        for(int i = 0;i<planeSO.Length;i++){
            if(planeSO[i].isSelected){
                selectBtn[i].SetActive(false);
                selectedTxt[i].SetActive(true);
            }
            else{
                if(planeSO[i].isPurchased){
                    selectBtn[i].SetActive(true);
                    selectedTxt[i].SetActive(false);
                }
            }
        }
    }
}
