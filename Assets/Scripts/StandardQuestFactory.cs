using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class StandardQuestFactory : IQuestFactory
{
    private GameObject defaultQuestPrefab;
    private GameObject dailyQuestPrefab;
    private GameObject epicQuestPrefab;

    public StandardQuestFactory(GameObject defaultPrefab, GameObject dailyPrefab, GameObject epicPrefab)
    {
        if (defaultPrefab == null || dailyPrefab == null || epicPrefab == null)
        {
            Debug.LogError("One or more prefabs are not assigned in the StandardQuestFactory constructor.");
        }

        this.defaultQuestPrefab = defaultPrefab;
        this.dailyQuestPrefab = dailyPrefab;
        this.epicQuestPrefab = epicPrefab;
    }

    public GameObject CreateQuestItem(QuestData questData, Transform parent)
    {
        // QuestData null kontrolü
        if (questData == null)
        {
            Debug.LogError("QuestData is null. Cannot create quest item.");
            return null;
        }

        // Parent null kontrolü
        if (parent == null)
        {
            Debug.LogError("Parent Transform is null. Cannot attach quest item.");
            return null;
        }

        GameObject prefabToUse = null;

        // Görev koşullarına göre prefab seçimi
        if (questData.goalCount >= 200)
        {
            prefabToUse = epicQuestPrefab;
        }
        else if (!string.IsNullOrEmpty(questData.questName) && questData.questName.Contains("Daily"))
        {
            prefabToUse = dailyQuestPrefab;
        }
        else
        {
            prefabToUse = defaultQuestPrefab;
        }

        // Prefab null kontrolü
        if (prefabToUse == null)
        {
            Debug.LogError("No valid prefab assigned in StandardQuestFactory. Check prefab references.");
            return null;
        }

        // Instantiate işlemi
        GameObject createdQuestItem = Object.Instantiate(prefabToUse, parent);

        if (createdQuestItem == null)
        {
            Debug.LogError("Failed to instantiate quest item prefab.");
            return null;
        }

        return createdQuestItem;
    }
}
