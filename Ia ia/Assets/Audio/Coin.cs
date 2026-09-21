using UnityEngine;
using StarterAssets;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ThirdPersonController player =
                other.GetComponent<ThirdPersonController>();

            if (player != null)
            {
                // MOEDA: continua contando normalmente
                PlayerObserverManager.AddCoin(player.playerID);

                // MOEDA: aumenta a velocidade do jogador
                player.IncreaseSpeed();

                Destroy(gameObject);
            }
        }
    }
}