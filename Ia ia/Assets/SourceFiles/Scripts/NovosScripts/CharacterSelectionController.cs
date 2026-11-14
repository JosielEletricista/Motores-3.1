using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro; // Necessário para usar TextMeshPro

public class CharacterSelectionController : MonoBehaviour
{
    [Header("Lista dos 5 Robôs Disponíveis")]
    [SerializeField] private List<RoboData> availableRobots = new List<RoboData>();

    [Header("UI - Jogador 1")]
    [SerializeField] private TextMeshProUGUI p1RobotNameText;
    [SerializeField] private TextMeshProUGUI p1StatusText;

    [Header("UI - Jogador 2")]
    [SerializeField] private TextMeshProUGUI p2RobotNameText;
    [SerializeField] private TextMeshProUGUI p2StatusText;

    // Armazena quem o Jogador 1 e Jogador 2 escolheram (índices da lista)
    private static int player1SelectedIndex = 0;
    private static int player2SelectedIndex = 0;

    private bool p1Confirmed = false;
    private bool p2Confirmed = false;

    private void Start()
    {
        // Atualiza a UI ao iniciar a cena
        UpdateUI();
    }

    // Métodos chamados pelos botões da UI do Jogador 1
    public void P1_NextRobot()
    {
        if (availableRobots.Count == 0 || p1Confirmed) return; // Impede mudar após confirmar
        player1SelectedIndex = (player1SelectedIndex + 1) % availableRobots.Count;
        UpdateUI();
        Debug.Log("Player 1 selecionou: " + availableRobots[player1SelectedIndex].roboName);
    }

    public void P1_PreviousRobot()
    {
        if (availableRobots.Count == 0 || p1Confirmed) return;
        player1SelectedIndex = (player1SelectedIndex - 1 + availableRobots.Count) % availableRobots.Count;
        UpdateUI();
        Debug.Log("Player 1 selecionou: " + availableRobots[player1SelectedIndex].roboName);
    }

    public void P1_Confirm()
    {
        if (availableRobots.Count == 0) return;
        p1Confirmed = true;
        UpdateUI();
        CheckStartGame();
    }

    // Métodos chamados pelos botões da UI do Jogador 2
    public void P2_NextRobot()
    {
        if (availableRobots.Count == 0 || p2Confirmed) return; // Impede mudar após confirmar
        player2SelectedIndex = (player2SelectedIndex + 1) % availableRobots.Count;
        UpdateUI();
        Debug.Log("Player 2 selecionou: " + availableRobots[player2SelectedIndex].roboName);
    }

    public void P2_PreviousRobot()
    {
        if (availableRobots.Count == 0 || p2Confirmed) return;
        player2SelectedIndex = (player2SelectedIndex - 1 + availableRobots.Count) % availableRobots.Count;
        UpdateUI();
        Debug.Log("Player 2 selecionou: " + availableRobots[player2SelectedIndex].roboName);
    }

    public void P2_Confirm()
    {
        if (availableRobots.Count == 0) return;
        p2Confirmed = true;
        UpdateUI();
        CheckStartGame();
    }

    // Método para atualizar os textos na tela
    private void UpdateUI()
    {
        if (availableRobots.Count > 0)
        {
            // Atualiza Nome do Jogador 1
            if (p1RobotNameText != null)
            {
                p1RobotNameText.text = availableRobots[player1SelectedIndex].roboName;
            }

            // Atualiza Nome do Jogador 2
            if (p2RobotNameText != null)
            {
                p2RobotNameText.text = availableRobots[player2SelectedIndex].roboName;
            }
        }

        // Atualiza Status de Confirmação do Jogador 1
        if (p1StatusText != null)
        {
            p1StatusText.text = p1Confirmed ? "CONFIRMADO!" : "Escolhendo...";
        }

        // Atualiza Status de Confirmação do Jogador 2
        if (p2StatusText != null)
        {
            p2StatusText.text = p2Confirmed ? "CONFIRMADO!" : "Escolhendo...";
        }
    }

    private void CheckStartGame()
    {
        // Só inicia se ambos confirmarem
        if (p1Confirmed && p2Confirmed)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadScene("GetStarted_Scene");
            }
            else
            {
                SceneManager.LoadScene("GetStarted_Scene");
            }
        }
    }

    // Métodos públicos para resgatar os robôs escolhidos nas outras cenas
    public static RoboData GetPlayer1Robo(List<RoboData> list)
    {
        if (list == null || list.Count == 0) return null;
        return list[Mathf.Clamp(player1SelectedIndex, 0, list.Count - 1)];
    }

    public static RoboData GetPlayer2Robo(List<RoboData> list, out bool isSameRobot)
    {
        isSameRobot = (player1SelectedIndex == player2SelectedIndex);
        if (list == null || list.Count == 0) return null;
        return list[Mathf.Clamp(player2SelectedIndex, 0, list.Count - 1)];
    }
}