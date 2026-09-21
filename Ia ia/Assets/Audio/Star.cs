using UnityEngine;
using StarterAssets;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ThirdPersonController player =
                other.GetComponent<ThirdPersonController>();

            if (player != null)
            {
                // Registra qual jogador coletou a estrela
                PlayerObserverManager.AddStar(player.playerID);

                // Informa ao StarManager que uma estrela foi coletada
                if (StarManager.Instance != null)
                {
                    StarManager.Instance.CollectStar();
                }

                Destroy(gameObject);
            }
        }
    }
}