using UnityEngine;

public class CommandFactory
{
    private HeroManager heroManager;

    public CommandFactory(HeroManager heroManager)
    {
        this.heroManager = heroManager;
    }

    public ICommand CreateCommand(string eventType, object data)
    {
        switch (eventType)
        {
            case "QuestCompleted":
                if (data is QuestData questData)
                {
                    return new QuestCompleteCommand(heroManager, questData.statName, questData.rewardAmount);
                }
                Debug.LogError("QuestCompleted event requires valid QuestData.");
                break;

            case "ResetStats":
                return new ResetStatsCommand(heroManager);

            default:
                Debug.LogWarning($"Unknown event type: {eventType}");
                break;
        }

        return null;
    }
}
