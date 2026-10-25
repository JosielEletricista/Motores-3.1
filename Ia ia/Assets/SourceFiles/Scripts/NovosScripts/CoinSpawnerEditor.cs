using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class CoinSpawnerEditor
{
    private static bool isSpawning = false;
    private static float spawnTimer = 0f;
    private const float spawnInterval = 5f;

    static CoinSpawnerEditor()
    {
        EditorApplication.update += OnEditorUpdate;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            // Verifica se estamos na cena de gameplay ao entrar no Play
            CheckScene(SceneManager.GetActiveScene().name);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (state == PlayModeStateChange.ExitingPlayMode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            isSpawning = false;
        }
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckScene(scene.name);
    }

    private static void CheckScene(string sceneName)
    {
        // Ativa o spawn apenas se estiver na cena de gameplay
        if (sceneName == "GetStarted_Scene")
        {
            isSpawning = true;
            spawnTimer = 0f;
            Debug.Log("[CoinSpawnerEditor] Modo de spawn automático de moedas ativado para a cena de gameplay.");
        }
        else
        {
            isSpawning = false;
        }
    }

    private static void OnEditorUpdate()
    {
        // Roda apenas durante o Play Mode
        if (!Application.isPlaying || !isSpawning) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnRandomCoin();
        }
    }

    private static void SpawnRandomCoin()
    {
        // Tenta carregar o prefab de moeda da pasta Resources (ex: Resources/Coin.prefab) 
        // ou você pode instanciar um primitivo básico caso o prefab não exista
        GameObject coinPrefab = Resources.Load<GameObject>("Coin");

        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * 15f;
        Vector3 spawnPosition = new Vector3(randomCircle.x, 1f, randomCircle.y);

        if (coinPrefab != null)
        {
            UnityEngine.Object.Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // Fallback caso não tenha o prefab na pasta Resources: cria uma esfera simples para representar a moeda
            GameObject tempCoin = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tempCoin.transform.position = spawnPosition;
            tempCoin.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            tempCoin.tag = "Coin"; // Certifique-se de ajustar conforme sua tag de colisão

            // Adiciona o script Coin se necessário
            if (tempCoin.GetComponent<Coin>() == null)
            {
                tempCoin.AddComponent<BoxCollider>().isTrigger = true;
            }

            Debug.Log("[CoinSpawnerEditor] Moeda gerada automaticamente em: " + spawnPosition);
        }
    }
}