using UnityEngine;
using TMPro;

public class StarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1StarText;
    [SerializeField] private TextMeshProUGUI player2StarText;

    private void OnEnable()
    {
        // NOVO: escuta o evento das estrelas
        PlayerObserverManager.OnStarCollected += UpdateUI;
    }

    private void OnDisable()
    {
        // NOVO: para de escutar o evento das estrelas
        PlayerObserverManager.OnStarCollected -= UpdateUI;
    }

    private void Start()
    {
        // NOVO: começa mostrando zero estrelas
        UpdateUI(0, 0);
    }

    private void UpdateUI(int player1Stars, int player2Stars)
    {
        player1StarText.text = "Estrelas: " + player1Stars;
        player2StarText.text = "Estrelas: " + player2Stars;
    }
}