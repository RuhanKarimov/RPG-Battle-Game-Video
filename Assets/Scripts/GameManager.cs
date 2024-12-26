using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HeroManager heroManager;
    private CommandFactory commandFactory;

    void Awake()
    {
        // HeroManager ve CommandFactory'i başlat
        heroManager = FindObjectOfType<HeroManager>();
        commandFactory = new CommandFactory(heroManager);

        // Tüm QuestItem'lere CommandFactory'i bağla
        QuestItem[] questItems = FindObjectsOfType<QuestItem>();
        foreach (var questItem in questItems)
        {
            questItem.SetCommandFactory(commandFactory);
        }
    }
}
