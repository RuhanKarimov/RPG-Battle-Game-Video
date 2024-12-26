using System;

[System.Serializable]
public class QuestData
{
    public string questName;
    public int goalCount;
    public string statName;
    public float rewardAmount;

    // Constructor
    public QuestData(string questName, int goalCount, string statName, float rewardAmount)
    {
        this.questName = questName;
        this.goalCount = goalCount;
        this.statName = statName;
        this.rewardAmount = rewardAmount;
    }
}
