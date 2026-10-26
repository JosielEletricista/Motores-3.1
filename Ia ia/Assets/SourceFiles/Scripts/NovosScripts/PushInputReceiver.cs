using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PushController))]
public class PushInputReceiver : MonoBehaviour
{
    private PushController pushController;

    private void Awake()
    {
        pushController = GetComponent<PushController>();
    }

    // Recebe a Action chamada "Push"
    public void OnPush(InputValue value)
    {
        if (value.isPressed)
        {
            pushController.TentarEmpurrar();
        }
    }
}