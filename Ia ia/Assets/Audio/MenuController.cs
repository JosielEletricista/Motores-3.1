using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.LoadScene("GetStarted_Scene");
    }

    public void QuitGame()
    {
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }
}