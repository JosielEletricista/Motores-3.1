using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notifica o sistema
            PlayerObserverManager.AddCoin();

            // Destroi a moeda
            Destroy(gameObject);
        }
    }
}