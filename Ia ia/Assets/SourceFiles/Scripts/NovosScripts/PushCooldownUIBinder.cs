
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PushCooldownUIBinder : MonoBehaviour
{
    [Header("Nomes dos Sliders na cena de GUI")]
    [SerializeField] private string nomeBarraP1 = "BarraPush_P1";
    [SerializeField] private string nomeBarraP2 = "BarraPush_P2";

    private bool conectadoP1;
    private bool conectadoP2;

    private IEnumerator Start()
    {
        float tempoEspera = 0f;

        // Aguarda até que os jogadores e a GUI estejam disponíveis
        while ((!conectadoP1 || !conectadoP2) && tempoEspera < 10f)
        {
            ConectarBarras();

            tempoEspera += Time.unscaledDeltaTime;
            yield return null;
        }

        if (!conectadoP1 || !conectadoP2)
        {
            Debug.LogWarning(
                "PushCooldownUIBinder: não foi possível encontrar " +
                "todos os jogadores ou barras de recarga."
            );
        }
    }

    private void ConectarBarras()
    {
        PushController[] jogadores =
            FindObjectsOfType<PushController>();

        Slider[] barras = FindObjectsOfType<Slider>();

        foreach (PushController jogador in jogadores)
        {
            if (jogador.PlayerID == 1 && !conectadoP1)
            {
                Slider barra = EncontrarBarra(barras, nomeBarraP1);

                if (barra != null)
                {
                    jogador.ConfigurarBarra(barra);
                    conectadoP1 = true;
                    Debug.Log("Barra de recarga do Player 1 conectada!");
                }
            }

            if (jogador.PlayerID == 2 && !conectadoP2)
            {
                Slider barra = EncontrarBarra(barras, nomeBarraP2);

                if (barra != null)
                {
                    jogador.ConfigurarBarra(barra);
                    conectadoP2 = true;
                    Debug.Log("Barra de recarga do Player 2 conectada!");
                }
            }
        }
    }

    private Slider EncontrarBarra(Slider[] barras, string nome)
    {
        foreach (Slider barra in barras)
        {
            if (barra.name == nome)
                return barra;
        }

        return null;
    }
}
