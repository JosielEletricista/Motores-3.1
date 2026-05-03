using UnityEngine;

public class SplashController : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(GoToMenu), 2f);
    }

    void GoToMenu()
    {
        GameManager.Instance.SetState(GameManager.GameState.MenuPrincipal);
        GameManager.Instance.LoadScene("MenuPrincipal");
    }
}