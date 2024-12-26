using UnityEngine;
using System.Collections.Generic;

public class HeroManager : MonoBehaviour
{
    public static HeroManager Instance;

    [Header("Hero Statları")]
    private Dictionary<string, float> stats = new Dictionary<string, float>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // HeroManager sahneler arasında taşınır
            InitializeDefaultStats();
        }
        else
        {
            Destroy(gameObject); // Tek bir HeroManager olmalı
        }
    }

    /// <summary>
    /// Varsayılan stat değerlerini başlatır.
    /// </summary>
    private void InitializeDefaultStats()
    {
        stats["melee"] = 20f;
        stats["magic"] = 50f;
        stats["magicrange"] = 25f;
        stats["speed"] = 10f;
        stats["experience"] = 0f;
        stats["health"] = 100f;
        stats["defense"] = 5f;
    }

    /// <summary>
    /// Belirtilen bir statı günceller.
    /// </summary>
    /// <param name="statName">Güncellenecek statın adı</param>
    /// <param name="amount">Eklenecek veya çıkarılacak miktar</param>
    public void UpdateStat(string statName, float amount)
    {
        if (stats.ContainsKey(statName.ToLower()))
        {
            stats[statName.ToLower()] += amount;
            Debug.Log($"{statName} güncellendi: {stats[statName.ToLower()]}");
        }
        else
        {
            Debug.LogWarning($"Bilinmeyen stat: {statName}");
        }
    }

    /// <summary>
    /// Belirtilen bir statın mevcut değerini döndürür.
    /// </summary>
    /// <param name="statName">Statın adı</param>
    /// <returns>Statın mevcut değeri</returns>
    public float GetStat(string statName)
    {
        if (stats.ContainsKey(statName.ToLower()))
        {
            return stats[statName.ToLower()];
        }
        else
        {
            Debug.LogWarning($"Bilinmeyen stat: {statName}");
            return 0f;
        }
    }

    /// <summary>
    /// Tüm statların kopyasını döndürür.
    /// </summary>
    /// <returns>Statların kopyası</returns>
    public Dictionary<string, float> GetAllStats()
    {
        return new Dictionary<string, float>(stats); // Statların kopyasını döndür
    }

    /// <summary>
    /// Tüm statları sıfırlar.
    /// </summary>
    public void ResetAllStats()
    {
        foreach (var key in stats.Keys)
        {
            stats[key] = 0f;
        }
        Debug.Log("Tüm statlar sıfırlandı.");
    }

    /// <summary>
    /// Belirli bir statı varsayılan değere sıfırlar.
    /// </summary>
    /// <param name="statName">Sıfırlanacak statın adı</param>
    public void ResetStat(string statName)
    {
        switch (statName.ToLower())
        {
            case "melee":
                stats["melee"] = 20f;
                break;
            case "magic":
                stats["magic"] = 50f;
                break;
            case "speed":
                stats["speed"] = 10f;
                break;
            case "magicrange":
                stats["magicrange"] = 25f;
                break;
            case "experience":
                stats["experience"] = 0f;
                break;
            case "health":
                stats["health"] = 100f;
                break;
            case "defense":
                stats["defense"] = 5f;
                break;
            default:
                Debug.LogWarning($"Bilinmeyen stat: {statName}");
                break;
        }
        Debug.Log($"{statName} varsayılan değere sıfırlandı.");
    }
}
