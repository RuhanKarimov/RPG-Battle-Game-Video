using UnityEngine;

public class QuestItem : MonoBehaviour
{
    [Header("Görev Bilgileri")]
    public string questName = "Varsayılan Görev";
    public int currentCount = 0;
    public int goalCount = 5;

    private bool isCompleted = false;

    [Header("Stat Değişiklikleri")]
    public float reward = 5;

    // Komut fabrikasını tutan alan
    private CommandFactory commandFactory;

    // Komut fabrikasını ayarlamak için kullanılan metod
    public void SetCommandFactory(CommandFactory factory)
    {
        this.commandFactory = factory;
    }

    public void IncrementProgress(int amount)
    {
        if (!isCompleted)
        {
            currentCount += amount;

            // Görev ilerlemesi güncelleme
            OnProgressUpdated();

            if (currentCount >= goalCount)
            {
                CompleteQuest();
            }
        }
    }

    private void CompleteQuest()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            Debug.Log($"Görev tamamlandı: {questName}");

            // Rastgele bir stat seç
            string selectedStat = SelectStat();

            // QuestData nesnesini oluştur ve komutu çalıştır
            QuestData questData = new QuestData(questName, goalCount, selectedStat, reward);

            ICommand command = commandFactory?.CreateCommand("QuestCompleted", questData);

            if (command != null)
            {
                command.Execute();
            }
            else
            {
                Debug.LogWarning("Komut oluşturulamadı.");
            }
        }
    }

    // Rastgele bir stat seçen metot
    private string SelectStat()
    {
        string[] stats = { "melee", "magic", "magicrange", "speed", "experience", "health", "defense" };
        int randomIndex = Random.Range(0, stats.Length);
        return stats[randomIndex];
    }

    // Görev ilerlemesi güncellendiğinde çağrılan metot
    public void OnProgressUpdated()
    {
    }
}
