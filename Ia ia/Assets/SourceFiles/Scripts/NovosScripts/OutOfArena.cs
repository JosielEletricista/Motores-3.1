
using UnityEngine;
using StarterAssets;

public class OutOfArena : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ThirdPersonController jogador =
            other.GetComponentInParent<ThirdPersonController>();

        if (jogador == null)
            return;

        if (RoundManager.Instance != null)
        {
            RoundManager.Instance.RegistrarQueda(jogador.playerID);
        }
    }
}
