using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScreen", LoadSceneMode.Single);
    }

    public void LoadQuestScene()
    {
        SceneManager.LoadScene("QuestScreen", LoadSceneMode.Single);
    }
}
