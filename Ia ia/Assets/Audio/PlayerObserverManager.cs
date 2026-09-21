using System;

public static class PlayerObserverManager
{
    public static Action<int, int> OnCoinCollected;

    // NOVO: evento separado para as estrelas
    public static Action<int, int> OnStarCollected;

    public static Action<string> OnGameFinished;

    private static int player1Coins = 0;
    private static int player2Coins = 0;

    // NOVO: quantidade de estrelas de cada jogador
    private static int player1Stars = 0;
    private static int player2Stars = 0;

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

    // NOVO: adiciona uma estrela para o jogador que coletou
    public static void AddStar(int playerID)
    {
        if (playerID == 1)
            player1Stars++;
        else if (playerID == 2)
            player2Stars++;

        OnStarCollected?.Invoke(player1Stars, player2Stars);
    }

    // NOVO: retorna as estrelas do Player 1
    public static int GetPlayer1Stars()
    {
        return player1Stars;
    }

    // NOVO: retorna as estrelas do Player 2
    public static int GetPlayer2Stars()
    {
        return player2Stars;
    }

    public static void FinishGame()
    {
        // NOVO: a vitória agora é determinada pela quantidade
        // de estrelas coletadas, e não pelas moedas
        if (player1Stars > player2Stars)
        {
            OnGameFinished?.Invoke("Player 1 venceu!");
        }
        else if (player2Stars > player1Stars)
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

    // NOVO: reseta as estrelas
    public static void ResetStars()
    {
        player1Stars = 0;
        player2Stars = 0;

        OnStarCollected?.Invoke(player1Stars, player2Stars);
    }
}