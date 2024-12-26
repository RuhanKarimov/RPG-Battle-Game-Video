using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestScreenUI : MonoBehaviour
{
    [Header("Görev Limiti")]
    public int maxDailyQuests = 3; // Maksimum günlük görev sayısı
    private int currentQuestCount = 0;

    [Header("Popup Mesaj Paneli")]
    public GameObject popupPanel;
    public Text popupMessageText;

    [Header("Quest Prefab ve Container")]
    public GameObject questItemPrefab;
    public GameObject dailyQuestPrefab;
    public GameObject epicQuestPrefab;
    public Transform questContainer;

    [Header("Yan Görev Ekle Butonu")]
    public Button addSideQuestButton;

    [Header("Varsayılan Görevler")]
    public List<QuestData> defaultQuests = new List<QuestData>();

    [Header("Yan Görev Havuzu (Side Quests)")]
    public List<QuestData> sideQuests = new List<QuestData>();

    private IQuestFactory questFactory;
    private CommandFactory commandFactory;
    private HeroManager heroManager;

    void Start()
    {
        popupPanel.SetActive(false);

        // HeroManager'ı bul ve başlat
        heroManager = FindObjectOfType<HeroManager>();
        if (heroManager == null)
        {
            Debug.LogError("HeroManager bulunamadı. Lütfen sahnede bir HeroManager nesnesi olduğundan emin olun.");
        }

        // Komut fabrikasını başlat
        InitializeCommandFactory();

        // Görev fabrikasını başlat
        questFactory = new StandardQuestFactory(questItemPrefab, dailyQuestPrefab, epicQuestPrefab);

        // Varsayılan görevleri tanımla
        InitializeDefaultQuests();

        // Yan görev havuzunu doldur
        InitializeSideQuests();

        // Varsayılan görevleri oluştur
        foreach (var questData in defaultQuests)
        {
            CreateQuestItem(questData);
        }

        if (addSideQuestButton != null)
        {
            addSideQuestButton.onClick.AddListener(OnAddSideQuestClicked);
        }
    }

    private void InitializeCommandFactory()
    {
        try
        {
            commandFactory = new CommandFactory(heroManager);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"CommandFactory oluşturulurken hata oluştu: {e.Message}");
        }
    }

    private void InitializeDefaultQuests()
    {
        // Varsayılan görevler listesi
        defaultQuests.Add(new QuestData("Barfiks çekmek", 50, "strength", 10));
        defaultQuests.Add(new QuestData("Mekik yapmak", 100, "defense", 15));
        defaultQuests.Add(new QuestData("Şınav yapmak", 75, "strength", 12));
    }

    private void InitializeSideQuests()
    {
        // Yan görevleri listeye ekle
        sideQuests.Add(new QuestData("Koşu yapmak", 200, "endurance", 20));
        sideQuests.Add(new QuestData("Squat yapmak", 80, "strength", 10));
        sideQuests.Add(new QuestData("Plank duruşu", 5, "endurance", 15));
        sideQuests.Add(new QuestData("Zıplama egzersizi", 120, "agility", 18));
        sideQuests.Add(new QuestData("Dumbbell kaldırmak", 40, "strength", 12));
        sideQuests.Add(new QuestData("Bisiklet sürmek", 10, "stamina", 8));
        sideQuests.Add(new QuestData("Yüzme", 100, "endurance", 25));
        sideQuests.Add(new QuestData("Yoga yapmak", 15, "flexibility", 10));
        sideQuests.Add(new QuestData("İp atlamak", 300, "agility", 22));
        sideQuests.Add(new QuestData("Lunge hareketi", 60, "strength", 14));
    }

    private void OnAddSideQuestClicked()
    {
        if (currentQuestCount >= maxDailyQuests)
        {
            ShowPopupMessage("Görev limitine ulaştınız!");
            return;
        }

        if (sideQuests.Count > 0)
        {
            int randomIndex = Random.Range(0, sideQuests.Count);
            QuestData randomQuest = sideQuests[randomIndex];
            CreateQuestItem(randomQuest);
            currentQuestCount++;
        }
    }

    private void CreateQuestItem(QuestData questData)
    {
        if (questFactory != null && questContainer != null)
        {
            GameObject questObj = questFactory.CreateQuestItem(questData, questContainer);

            QuestItem questItem = questObj.GetComponent<QuestItem>();
            if (questItem != null)
            {
                questItem.questName = questData.questName;
                questItem.goalCount = questData.goalCount;
                questItem.SetCommandFactory(commandFactory);
            }
        }
    }

    private void ShowPopupMessage(string message)
    {
        if (popupPanel != null && popupMessageText != null)
        {
            popupPanel.SetActive(true);
            popupMessageText.text = message;
            Invoke("HidePopup", 3f);
        }
    }

    private void HidePopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }
}
