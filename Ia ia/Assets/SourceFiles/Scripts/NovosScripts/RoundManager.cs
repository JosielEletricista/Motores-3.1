
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }

    public static int Player1Rounds { get; private set; }
    public static int Player2Rounds { get; private set; }
    public static int WinnerPlayerID { get; private set; }
    public static string Player1Name { get; private set; } = "Player 1";
    public static string Player2Name { get; private set; } = "Player 2";

    [Header("Posições iniciais")]
    [SerializeField] private Transform spawnP1;
    [SerializeField] private Transform spawnP2;

    [Header("Configuração da partida")]
    [SerializeField] private int roundsParaVencer = 2;
    [SerializeField] private float tempoAntesDeReiniciar = 2f;

    [Header("Cena de vitória (opcional)")]
    [SerializeField] private string cenaVitoria = "";

    private ThirdPersonController player1;
    private ThirdPersonController player2;

    private bool roundEncerrado;
    private bool partidaEncerrada;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // NOVO: começa uma partida com o placar zerado
        Player1Rounds = 0;
        Player2Rounds = 0;
        WinnerPlayerID = 0;
    }

    private void Start()
    {
        EncontrarJogadores();
    }

    private void EncontrarJogadores()
    {
        ThirdPersonController[] jogadores =
            FindObjectsOfType<ThirdPersonController>();

        foreach (ThirdPersonController jogador in jogadores)
        {
            if (jogador.playerID == 1)
                player1 = jogador;
            else if (jogador.playerID == 2)
                player2 = jogador;
        }
        // NOVO: obtém os nomes dos robôs selecionados
        if (player1 != null)
        {
            RoboSetup setup = player1.GetComponent<RoboSetup>();

            if (setup != null && setup.GetAssignedData() != null)
                Player1Name = setup.GetAssignedData().roboName;
        }

        if (player2 != null)
        {
            RoboSetup setup = player2.GetComponent<RoboSetup>();

            if (setup != null && setup.GetAssignedData() != null)
                Player2Name = setup.GetAssignedData().roboName;
        }
    }

    public void RegistrarQueda(int jogadorQueCaiu)
    {
        if (roundEncerrado || partidaEncerrada)
            return;

        if (jogadorQueCaiu != 1 && jogadorQueCaiu != 2)
            return;

        roundEncerrado = true;

        // Quem derrubou o adversário ganha o round
        if (jogadorQueCaiu == 1)
            Player2Rounds++;
        else
            Player1Rounds++;

        Debug.Log(
            "Placar: P1 " + Player1Rounds +
            " x " + Player2Rounds + " P2"
        );

        // Verifica se alguém ganhou a partida
        if (Player1Rounds >= roundsParaVencer)
        {
            EncerrarPartida(1);
            return;
        }

        if (Player2Rounds >= roundsParaVencer)
        {
            EncerrarPartida(2);
            return;
        }

        // Ainda há rounds: prepara a próxima rodada
        StartCoroutine(ReiniciarRound());
    }

    private IEnumerator ReiniciarRound()
    {
        yield return new WaitForSeconds(tempoAntesDeReiniciar);

        EncontrarJogadores();

        ReposicionarJogador(player1, spawnP1);
        ReposicionarJogador(player2, spawnP2);

        roundEncerrado = false;
    }

    private void ReposicionarJogador(
        ThirdPersonController jogador,
        Transform pontoInicial)
    {
        if (jogador == null || pontoInicial == null)
        {
            Debug.LogWarning(
                "RoundManager: jogador ou posição inicial não configurados."
            );
            return;
        }

        CharacterController characterController =
            jogador.GetComponent<CharacterController>();

        // Desativa o CharacterController para teleportar sem colisões
        if (characterController != null)
            characterController.enabled = false;

        jogador.transform.SetPositionAndRotation(
            pontoInicial.position,
            pontoInicial.rotation
        );

        if (characterController != null)
            characterController.enabled = true;

        // Zera o impulso restante do round anterior
        RobotKnockback knockback =
            jogador.GetComponent<RobotKnockback>();

        if (knockback != null)
            knockback.ResetKnockback();
    }

    private void EncerrarPartida(int vencedor)
    {
        partidaEncerrada = true;
        WinnerPlayerID = vencedor;

        Debug.Log("Jogador " + vencedor + " venceu a partida!");

        StartCoroutine(VoltarParaSelecao());
    }

    private IEnumerator VoltarParaSelecao()
    {
        // Mantém a mensagem de vitória visível por 3 segundos
        yield return new WaitForSeconds(3f);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene("CharacterSelection_Scene");
        }
        else
        {
            SceneManager.LoadScene("CharacterSelection_Scene");
        }
    }
}