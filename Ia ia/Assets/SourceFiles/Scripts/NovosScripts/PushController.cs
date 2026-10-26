using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class PushController : MonoBehaviour
{
    [Header("Jogador")]
    [SerializeField] private int playerID = 1;

    [Header("Empurrão")]
    [SerializeField] private float alcance = 2.5f;
    [SerializeField] private float tempoRecarga = 2f;
    [SerializeField] private float forcaVertical = 5f;

    [Header("Interface")]
    [SerializeField] private Slider barraRecarga;

    private ThirdPersonController meuControle;
    private RoboSetup meuSetup;
    private float tempoRestante;

    private void Awake()
    {
        meuControle = GetComponent<ThirdPersonController>();
        meuSetup = GetComponent<RoboSetup>();

        if (meuControle != null)
        {
            playerID = meuControle.playerID;
        }

        AtualizarBarra();
    }

    private void Update()
    {
        if (tempoRestante > 0f)
        {
            tempoRestante -= Time.deltaTime;
            tempoRestante = Mathf.Max(0f, tempoRestante);
            AtualizarBarra();
        }
    }

    public void TentarEmpurrar()
    {
        if (tempoRestante > 0f)
            return;

        ThirdPersonController[] jogadores =
            FindObjectsOfType<ThirdPersonController>();

        foreach (ThirdPersonController adversario in jogadores)
        {
            if (adversario == meuControle)
                continue;

            if (adversario.playerID == playerID)
                continue;

            Vector3 diferenca =
                adversario.transform.position - transform.position;

            float distancia = diferenca.magnitude;

            if (distancia > alcance || distancia <= 0.01f)
                continue;

            // O adversário precisa estar à frente do atacante
            Vector3 frente = transform.forward;
            frente.y = 0f;
            frente.Normalize();

            Vector3 direcaoAdversario = diferenca.normalized;

            if (Vector3.Dot(frente, direcaoAdversario) < 0.25f)
                continue;

            RobotKnockback knockback =
                adversario.GetComponent<RobotKnockback>();

            if (knockback == null)
            {
                Debug.LogWarning(
                    "Adicione RobotKnockback ao robô adversário."
                );
                return;
            }

            // Lê a força definida no RoboData selecionado
            float multiplicador = 10f;

            if (meuSetup != null && meuSetup.GetAssignedData() != null)
            {
                multiplicador =
                    meuSetup.GetAssignedData().pushForceMultiplier;
            }

            // Quanto mais perto o adversário, maior a força
            float proximidade = 1f - Mathf.Clamp01(distancia / alcance);
            float forca = multiplicador * Mathf.Lerp(0.7f, 1.4f, proximidade);

            knockback.AplicarImpulso(
                frente,
                forca,
                forcaVertical * Mathf.Lerp(0.7f, 1.3f, proximidade)
            );

            tempoRestante = tempoRecarga;
            AtualizarBarra();

            return;
        }
    }

        // NOVO: permite conectar a barra da cena de GUI
    public int PlayerID => playerID;

    public void ConfigurarBarra(Slider barra)
    {
        barraRecarga = barra;
        AtualizarBarra();
}

    private void AtualizarBarra()
    {
        if (barraRecarga == null)
            return;

        barraRecarga.minValue = 0f;
        barraRecarga.maxValue = 1f;

        // 1 = pronto para empurrar; 0 = recarregando
        barraRecarga.value = tempoRecarga > 0f
            ? 1f - tempoRestante / tempoRecarga
            : 1f;
    }
}