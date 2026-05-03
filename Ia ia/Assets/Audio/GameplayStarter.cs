using UnityEngine;

public class GameplayStarter : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.SetState(GameManager.GameState.Gameplay);
    }
}