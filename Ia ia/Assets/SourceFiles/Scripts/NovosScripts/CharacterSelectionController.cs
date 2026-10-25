using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CharacterSelectionController : MonoBehaviour
{
    [Header("Lista dos 5 Robôs Disponíveis")]
    [SerializeField] private List<RoboData> availableRobots = new List<RoboData>();

    // Armazena quem o Jogador 1 e Jogador 2 escolheram (índices da lista)
    private static int player1SelectedIndex = 0;
    private static int player2SelectedIndex = 0;

    private bool p1Confirmed = false;
    private bool p2Confirmed = false;

    // Métodos chamados pelos botões da UI do Jogador 1
    public void P1_NextRobot()
    {
        if (availableRobots.Count == 0) return;
        player1SelectedIndex = (player1SelectedIndex + 1) % availableRobots.Count;
        Debug.Log("Player 1 selecionou: " + availableRobots[player1SelectedIndex].roboName);
    }

    public void P1_PreviousRobot()
    {
        if (availableRobots.Count == 0) return;
        player1SelectedIndex = (player1SelectedIndex - 1 + availableRobots.Count) % availableRobots.Count;
        Debug.Log("Player 1 selecionou: " + availableRobots[player1SelectedIndex].roboName);
    }

    public void P1_Confirm()
    {
        p1Confirmed = true;
        CheckStartGame();
    }

    // Métodos chamados pelos botões da UI do Jogador 2
    public void P2_NextRobot()
    {
        if (availableRobots.Count == 0) return;
        player2SelectedIndex = (player2SelectedIndex + 1) % availableRobots.Count;
        Debug.Log("Player 2 selecionou: " + availableRobots[player2SelectedIndex].roboName);
    }

    public void P2_PreviousRobot()
    {
        if (availableRobots.Count == 0) return;
        player2SelectedIndex = (player2SelectedIndex - 1 + availableRobots.Count) % availableRobots.Count;
        Debug.Log("Player 2 selecionou: " + availableRobots[player2SelectedIndex].roboName);
    }

    public void P2_Confirm()
    {
        p2Confirmed = true;
        CheckStartGame();
    }

    private void CheckStartGame()
    {
        // Só inicia se ambos confirmarem
        if (p1Confirmed && p2Confirmed)
        {
            // Salva as escolhas estaticamente para o GameManager ou para a cena de gameplay ler
            // Se ambos escolheram o mesmo robô, a regra de cor secundária será aplicada no setup do robô
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