using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;

public class CameraMultiplayerSetup : MonoBehaviour
{
    private void Start()
    {
        ThirdPersonController[] players =
            FindObjectsOfType<ThirdPersonController>();

        foreach (ThirdPersonController player in players)
        {
            SetupPlayerCamera(player);
        }
    }

    private void SetupPlayerCamera(ThirdPersonController player)
    {
        int id = player.playerID;

        // Procura o PlayerCameraRoot dentro do jogador
        Transform cameraRoot = null;

        Transform[] children =
            player.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.name == "PlayerCameraRoot")
            {
                cameraRoot = child;
                break;
            }
        }

        if (cameraRoot == null)
        {
            Debug.LogError(
                "PlayerCameraRoot não encontrado no Player " + id
            );
            return;
        }

        // Procura a Cinemachine Camera dentro do jogador
        CinemachineCamera cmCamera =
            player.GetComponentInChildren<CinemachineCamera>(true);

        if (cmCamera == null)
        {
            Debug.LogError(
                "Cinemachine Camera não encontrada no Player " + id
            );
            return;
        }

        // Define o jogador que essa câmera deve seguir
        cmCamera.Follow = cameraRoot;
        cmCamera.LookAt = cameraRoot;

        Debug.Log(
            "Câmera do Player " + id +
            " configurada para seguir " + cameraRoot.name
        );
    }
}