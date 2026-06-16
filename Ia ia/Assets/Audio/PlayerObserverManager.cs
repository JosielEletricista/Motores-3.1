using System;

public static class PlayerObserverManager
{
    // Evento chamado quando uma moeda é coletada
    public static Action<int> OnCoinCollected;

    private static int coinCount = 0;

    public static void AddCoin()
    {
        coinCount++;
        OnCoinCollected?.Invoke(coinCount);
    }

    public static void ResetCoins()
    {
        coinCount = 0;
        OnCoinCollected?.Invoke(coinCount);
    }
}