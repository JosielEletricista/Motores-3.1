using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

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
        UpdateUI(0); // começa em 0
    }

    void UpdateUI(int value)
    {
        coinText.text = "Estrelas: " + value;
    }
}