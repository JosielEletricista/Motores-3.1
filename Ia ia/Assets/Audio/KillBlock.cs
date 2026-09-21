using UnityEngine;
using StarterAssets;

public class KillBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer respawn =
                other.GetComponent<RespawnPlayer>();

            if (respawn != null)
            {
                // Usa o sistema de respawn que já existe no Player
                respawn.Respawn();
            }
        }
    }
}