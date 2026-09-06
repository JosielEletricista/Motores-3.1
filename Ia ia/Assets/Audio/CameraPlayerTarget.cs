using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;

public class CameraPlayerTarget : MonoBehaviour
{
    [SerializeField] private int playerID = 1;

    private CinemachineCamera cinemachineCamera;

    private void Start()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();

        ThirdPersonController[] players =
            FindObjectsOfType<ThirdPersonController>();

        foreach (ThirdPersonController player in players)
        {
            if (player.playerID == playerID)
            {
                Transform[] children =
                    player.GetComponentsInChildren<Transform>();

                foreach (Transform child in children)
                {
                    if (child.name == "PlayerCameraRoot")
                    {
                        cinemachineCamera.Follow = child;
                        cinemachineCamera.LookAt = child;

                        Debug.Log(
                            "Câmera configurada para Player " + playerID
                        );

                        return;
                    }
                }
            }
        }

        Debug.LogWarning(
            "PlayerCameraRoot não encontrado para Player " + playerID
        );
    }
}