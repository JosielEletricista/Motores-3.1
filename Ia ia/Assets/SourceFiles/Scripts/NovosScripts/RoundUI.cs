
using UnityEngine;
using TMPro;

public class RoundsUI : MonoBehaviour
{
    [Header("Textos da interface")]
    [SerializeField] private TMP_Text textoP1;
    [SerializeField] private TMP_Text textoP2;
    [SerializeField] private TMP_Text textoResultado;

    private void Update()
    {
        if (RoundManager.Instance == null)
            return;

        // Exibe o nome do robô e seus rounds vencidos
        if (textoP1 != null)
        {
            textoP1.text =
                RoundManager.Player1Name + ": " +
                RoundManager.Player1Rounds;
        }

        if (textoP2 != null)
        {
            textoP2.text =
                RoundManager.Player2Name + ": " +
                RoundManager.Player2Rounds;
        }

        // Exibe o nome do robô vencedor
        if (textoResultado != null)
        {
            if (RoundManager.WinnerPlayerID == 1)
            {
                textoResultado.text =
                    RoundManager.Player1Name +
                    " venceu a partida!";
            }
            else if (RoundManager.WinnerPlayerID == 2)
            {
                textoResultado.text =
                    RoundManager.Player2Name +
                    " venceu a partida!";
            }
            else
            {
                textoResultado.text = "";
            }
        }
    }
}
