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
                PlayerObserverManager.AddCoin(player.playerID);
                player.IncreaseSpeed();

                Destroy(gameObject);

                Invoke(nameof(CheckGameFinished), 0.1f);
            }
        }
    }

    private void CheckGameFinished()
    {
        Coin[] remainingCoins = FindObjectsByType<Coin>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );

        if (remainingCoins.Length == 0)
        {
            PlayerObserverManager.FinishGame();
        }
    }
}