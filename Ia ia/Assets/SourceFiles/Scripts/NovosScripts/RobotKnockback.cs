using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RobotKnockback : MonoBehaviour
{
    [Header("Configuração do Impulso")]
    [SerializeField] private float gravidade = -20f;
    [SerializeField] private float desaceleracao = 12f;

    private CharacterController characterController;
    private Vector3 velocidadeImpulso;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void AplicarImpulso(Vector3 direcao, float forca, float forcaVertical)
    {
        direcao.y = 0f;
        direcao.Normalize();

        velocidadeImpulso = direcao * forca;
        velocidadeImpulso.y = forcaVertical;
    }

    private void Update()
    {
        if (velocidadeImpulso.sqrMagnitude < 0.01f)
        {
            velocidadeImpulso = Vector3.zero;
            return;
        }

        // Desacelera o impulso horizontal
        Vector3 velocidadeHorizontal = new Vector3(
            velocidadeImpulso.x, 0f, velocidadeImpulso.z
        );

        velocidadeHorizontal = Vector3.MoveTowards(
            velocidadeHorizontal,
            Vector3.zero,
            desaceleracao * Time.deltaTime
        );

        velocidadeImpulso.x = velocidadeHorizontal.x;
        velocidadeImpulso.z = velocidadeHorizontal.z;

        // Gravidade cria o arco do empurrão
        velocidadeImpulso.y += gravidade * Time.deltaTime;

        characterController.Move(velocidadeImpulso * Time.deltaTime);

        // Interrompe o impulso vertical ao tocar o chão
        if (characterController.isGrounded && velocidadeImpulso.y < 0f)
        {
            velocidadeImpulso.y = 0f;
        }
    }
}