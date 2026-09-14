using UnityEngine;
using StarterAssets;

public class Coin : MonoBehaviour
{
    private static int totalCoins;
    private static int collectedCoins;

    private void Start()
    {
        totalCoins++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        ThirdPersonController player =
            other.GetComponent<ThirdPersonController>();

        if (player == null)
            return;

        // Adiciona a estrela ao jogador
        PlayerObserverManager.AddCoin(player.playerID);

        // Aumenta a velocidade
        player.IncreaseSpeed();

        // Conta a estrela coletada
        collectedCoins++;

        // Remove a estrela
        Destroy(gameObject);

        Debug.Log(
            "Estrelas coletadas: " + collectedCoins +
            " / " + totalCoins
        );

        // Verifica se todas foram coletadas
        if (collectedCoins >= totalCoins)
        {
            Debug.Log("TODAS AS ESTRELAS FORAM COLETADAS!");

            PlayerObserverManager.FinishGame();
        }
    }
}