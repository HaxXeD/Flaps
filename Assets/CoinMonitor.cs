using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class CoinMonitor : MonoBehaviour
{
    TMP_Text coinTxt;
    public UnityEvent coinText = new UnityEvent();
    int totalcoin;

    public int TotalCoin{
        get {return totalcoin;}
        set {totalcoin = value;}
        
    }
    // Start is called before the first frame update
    void Start()
    {
        totalcoin = PlayerPrefs.GetInt("totalcoin",0);
        coinTxt = GetComponent<TMP_Text>();
        coinTxt.text  = TotalCoin.ToString();
        coinText.AddListener(UpdateCoinText);
    }

    private void UpdateCoinText()
    {
        PlayerPrefs.SetInt("totalcoin",TotalCoin);
        coinTxt.text = TotalCoin.ToString();
    }
}
