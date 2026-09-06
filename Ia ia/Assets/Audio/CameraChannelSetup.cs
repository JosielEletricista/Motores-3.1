using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;

public class CameraChannelSetup : MonoBehaviour
{
    [SerializeField] private int playerID = 1;

    private void Start()
    {
        CinemachineBrain brain =
            GetComponent<CinemachineBrain>();

        if (brain == null)
        {
            Debug.LogError(
                "Cinemachine Brain não encontrado na câmera do Player "
                + playerID
            );

            return;
        }

        if (playerID == 1)
        {
            brain.ChannelMask = OutputChannels.Channel01;
        }
        else if (playerID == 2)
        {
            brain.ChannelMask = OutputChannels.Channel02;
        }
    }
}