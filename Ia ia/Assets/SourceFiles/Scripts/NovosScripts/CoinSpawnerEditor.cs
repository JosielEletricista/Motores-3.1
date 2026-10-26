
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinSpawner : MonoBehaviour
{
    [Header("Configuração das moedas")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnRadius = 15f;
    [SerializeField] private float spawnHeight = 1f;

    [Header("Limite de moedas")]
    [SerializeField] private int maxCoins = 10;

    private int moedasAtuais = 0;

    private void Start()
    {
        // Só executa na cena de gameplay
        if (gameObject.scene.name != "GetStarted_Scene")
        {
            Debug.LogWarning(
                "CoinSpawner deve estar na cena GetStarted_Scene."
            );
            enabled = false;
            return;
        }

        // Tenta carregar o prefab de Resources se não foi atribuído
        if (coinPrefab == null)
        {
            coinPrefab = Resources.Load<GameObject>("Coin");
        }

        if (coinPrefab == null)
        {
            Debug.LogError(
                "CoinSpawner: prefab da moeda não encontrado! " +
                "Arraste o prefab Coin para o campo Coin Prefab."
            );
            enabled = false;
            return;
        }

        StartCoroutine(SpawnCoinsRoutine());
    }

    private IEnumerator SpawnCoinsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (moedasAtuais < maxCoins)
            {
                SpawnCoin();
            }
        }
    }

    private void SpawnCoin()
    {
        Vector2 randomPosition =
            Random.insideUnitCircle * spawnRadius;

        Vector3 spawnPosition = transform.position +
            new Vector3(
                randomPosition.x,
                spawnHeight,
                randomPosition.y
            );

        GameObject moeda = Instantiate(
            coinPrefab,
            spawnPosition,
            Quaternion.identity
        );

        moedasAtuais++;

        // Decrementa quando a moeda for destruída após a coleta
        CoinSpawnTracker tracker =
            moeda.AddComponent<CoinSpawnTracker>();

        tracker.Configurar(this);

        Debug.Log("Moeda criada em: " + spawnPosition);
    }

    public void MoedaRemovida()
    {
        moedasAtuais = Mathf.Max(0, moedasAtuais - 1);
    }
}

// Componente auxiliar para acompanhar as moedas geradas
public class CoinSpawnTracker : MonoBehaviour
{
    private CoinSpawner spawner;
    private bool contabilizada = true;

    public void Configurar(CoinSpawner coinSpawner)
    {
        spawner = coinSpawner;
    }

    private void OnDestroy()
    {
        if (contabilizada && spawner != null)
        {
            spawner.MoedaRemovida();
        }
    }
}
