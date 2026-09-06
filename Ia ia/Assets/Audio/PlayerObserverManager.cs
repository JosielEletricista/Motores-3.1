using System;

public static class PlayerObserverManager
{
    public static Action<int, int> OnCoinCollected;
    public static Action<string> OnGameFinished;

    private static int player1Coins = 0;
    private static int player2Coins = 0;

    public static void AddCoin(int playerID)
    {
        if (playerID == 1)
            player1Coins++;
        else if (playerID == 2)
            player2Coins++;

        OnCoinCollected?.Invoke(player1Coins, player2Coins);
    }

    public static int GetPlayer1Coins()
    {
        return player1Coins;
    }

    public static int GetPlayer2Coins()
    {
        return player2Coins;
    }

    public static void FinishGame()
    {
        if (player1Coins > player2Coins)
        {
            OnGameFinished?.Invoke("Player 1 venceu!");
        }
        else if (player2Coins > player1Coins)
        {
            OnGameFinished?.Invoke("Player 2 venceu!");
        }
        else
        {
            OnGameFinished?.Invoke("Empate!");
        }
    }

    public static void ResetCoins()
    {
        player1Coins = 0;
        player2Coins = 0;

        OnCoinCollected?.Invoke(player1Coins, player2Coins);
    }
}