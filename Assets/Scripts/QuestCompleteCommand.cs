using UnityEngine;

public class QuestCompleteCommand : ICommand
{
    private HeroManager heroManager;
    private string statName;
    private float rewardAmount;

    public QuestCompleteCommand(HeroManager heroManager, string statName, float rewardAmount)
    {
        this.heroManager = heroManager;
        this.statName = statName;
        this.rewardAmount = rewardAmount;
    }

    public void Execute()
    {
        heroManager.UpdateStat(statName, rewardAmount);
        Debug.Log($"Stat Güncellendi: {statName} +{rewardAmount}");
    }
}
