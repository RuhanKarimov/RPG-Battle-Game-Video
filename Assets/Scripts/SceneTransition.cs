using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [Tooltip("Görevi increment veya tamamla butonu.")]
    public Button playButton;

    private ISceneState currentState;

    void Start()
    {
        // Mevcut sahneye uygun durumu ayarla
        SetCurrentState();

        // Butona tıklama davranışını bağla
        if (playButton != null)
        {
            playButton.onClick.AddListener(() => currentState.HandlePlayButton(this));
        }
    }

    private void SetCurrentState()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // Sahneye göre durumu seç
        if (currentScene == "QuestScreen")
        {
            currentState = new QuestScreenState();
        }
        else if (currentScene == "BattleScreen")
        {
            currentState = new BattleScreenState();
        }
        else
        {
            Debug.LogError($"No state defined for scene: {currentScene}");
        }
    }
}
