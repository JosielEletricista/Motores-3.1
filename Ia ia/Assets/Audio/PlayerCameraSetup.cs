using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;

public class PlayerCameraSetup : MonoBehaviour
{
    [SerializeField] private int playerID = 1;

    private void Awake()
    {
        SetupCamera();
    }

    private void SetupCamera()
    {
        // Procura os componentes SOMENTE dentro deste PlayerRobot
        CinemachineCamera virtualCamera =
            GetComponentInChildren<CinemachineCamera>(true);

        Camera physicalCamera =
            GetComponentInChildren<Camera>(true);

        if (virtualCamera == null)
        {
            Debug.LogError("Cinemachine Camera não encontrada no Player " + playerID);
            return;
        }

        if (physicalCamera == null)
        {
            Debug.LogError("Camera física não encontrada no Player " + playerID);
            return;
        }

        // Procura o PlayerCameraRoot deste mesmo robô
        Transform cameraRoot = null;

        Transform[] transforms =
            GetComponentsInChildren<Transform>(true);

        foreach (Transform t in transforms)
        {
            if (t.name == "PlayerCameraRoot")
            {
                cameraRoot = t;
                break;
            }
        }

        if (cameraRoot == null)
        {
            Debug.LogError("PlayerCameraRoot não encontrado no Player " + playerID);
            return;
        }

        // Cada câmera segue seu próprio personagem
        virtualCamera.Follow = cameraRoot;
        virtualCamera.LookAt = cameraRoot;

        // Cada câmera usa um canal diferente
        if (playerID == 1)
        {
            virtualCamera.OutputChannel = OutputChannels.Channel01;
        }
        else if (playerID == 2)
        {
            virtualCamera.OutputChannel = OutputChannels.Channel02;
        }

        // Configura o Brain da câmera física
        CinemachineBrain brain =
            physicalCamera.GetComponent<CinemachineBrain>();

        if (brain != null)
        {
            if (playerID == 1)
                brain.ChannelMask = OutputChannels.Channel01;

            if (playerID == 2)
                brain.ChannelMask = OutputChannels.Channel02;
        }

        // Garante que a câmera física esteja ativa
        physicalCamera.enabled = true;

        Debug.Log(
            "Câmera configurada para Player " +
            playerID +
            " -> " +
            cameraRoot.name
        );
    }
}