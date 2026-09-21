using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1CoinText;
    [SerializeField] private TextMeshProUGUI player2CoinText;

    private void OnEnable()
    {
        PlayerObserverManager.OnCoinCollected += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinCollected -= UpdateUI;
    }

    private void Start()
    {
        UpdateUI(0, 0);
    }

    private void UpdateUI(int player1Coins, int player2Coins)
    {
        // CORRIGIDO: este contador é de MOEDAS
        player1CoinText.text = "Moedas: " + player1Coins;
        player2CoinText.text = "Moedas: " + player2Coins;
    }
}