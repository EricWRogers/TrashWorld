using UnityEngine;
using TMPro;

public class CoinUi : MonoBehaviour
{
  [SerializeField] private TMP_Text coinText;

  private void Update(){
    if(GameManager.instance != null){
      coinText.text = "Coins: " + GameManager.instance.coins;
    }
  }
}
