using UnityEngine;
using StarterAssets;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance;

    [Header("Star")]
    [SerializeField] private GameObject starPrefab;

    private int totalStars;
    private int collectedStars;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Conta todas as estrelas colocadas manualmente na cena
        totalStars = FindObjectsByType<Star>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        ).Length;

        collectedStars = 0;

        Debug.Log("Total de estrelas: " + totalStars);
    }

    public void CollectStar()
    {
        collectedStars++;

        Debug.Log(
            "Estrelas coletadas: " +
            collectedStars +
            "/" +
            totalStars
        );

        // Quando todas as estrelas forem coletadas
        if (collectedStars >= totalStars)
        {
            PlayerObserverManager.FinishGame();
        }
    }

    public int GetTotalStars()
    {
        return totalStars;
    }

    public int GetCollectedStars()
    {
        return collectedStars;
    }
}