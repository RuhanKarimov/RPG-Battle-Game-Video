using UnityEngine;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    [Header("Quest Mantık Script'i")]
    public QuestItem questItem;

    [Header("UI Referansları")]
    public Text questNameText;
    public Text progressText;
    public Button completeButton;

    void Start()
    {
        if (questItem != null)
        {
            // Görev ilerlemesi güncellendiğinde manuel olarak güncelleme
            UpdateUI();

            if (completeButton != null)
            {
                completeButton.onClick.AddListener(() =>
                {
                    questItem.IncrementProgress(5);
                    questItem.OnProgressUpdated(); // İlerleme değişikliklerini manuel tetikleme
                    UpdateUI();
                });
            }
        }
    }

    private void UpdateUI()
    {
        if (questItem != null && questNameText != null)
        {
            questNameText.text = questItem.questName;
            progressText.text = $"{questItem.currentCount} / {questItem.goalCount}";
        }
    }
}
