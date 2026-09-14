using UnityEngine;
using TMPro;

public class WinnerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;

    private void OnEnable()
    {
        PlayerObserverManager.OnGameFinished += ShowWinner;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnGameFinished -= ShowWinner;
    }

    private void Start()
    {
        winnerText.text = "";
    }

    private void ShowWinner(string message)
    {
        winnerText.text = message;
    }
}
