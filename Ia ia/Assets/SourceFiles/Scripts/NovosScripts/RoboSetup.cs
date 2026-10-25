using UnityEngine;
using StarterAssets; // Namespace comum do seu Starter Assets usado nas referências anteriores

public class RoboSetup : MonoBehaviour
{
    [SerializeField] private int playerID = 1; // 1 para Player 1, 2 para Player 2
    [Header("Arraste aqui a mesma lista de 5 RoboData da seleção")]
    [SerializeField] private RoboData[] allRobots;

    private RoboData assignedData;

    private void Awake()
    {
        ConfigureRobot();
    }

    private void ConfigureRobot()
    {
        if (allRobots == null || allRobots.Length == 0) return;

        bool isSame = false;
        if (playerID == 1)
        {
            assignedData = CharacterSelectionController.GetPlayer1Robo(new System.Collections.Generic.List<RoboData>(allRobots));
        }
        else
        {
            assignedData = CharacterSelectionController.GetPlayer2Robo(new System.Collections.Generic.List<RoboData>(allRobots), out isSame);
        }

        if (assignedData == null) return;

        // 1. Aplica o Tamanho (Scale)
        transform.localScale = Vector3.one * assignedData.scale;

        // 2. Aplica a Cor (Se ambos escolheram o mesmo robô, o Player 2 pega a cor secundária)
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        Color targetColor = (playerID == 2 && isSame) ? assignedData.secondaryColor : assignedData.primaryColor;

        foreach (var rend in renderers)
        {
            rend.material.color = targetColor;
        }

        // 3. Aplica os Status no Controlador do Player (ex: ThirdPersonController ou equivalente)
        ThirdPersonController controller = GetComponent<ThirdPersonController>();
        if (controller != null)
        {
            // Ajusta a velocidade base herdada do RoboData
            // (Certifique-se de que seu ThirdPersonController possua uma propriedade ou método para ajustar a velocidade)
            controller.MoveSpeed = assignedData.baseSpeed;
        }

        Debug.Log("Robô do Player " + playerID + " configurado como: " + assignedData.roboName + " | Cor aplicada: " + targetColor);
    }

    public RoboData GetAssignedData()
    {
        return assignedData;
    }
}