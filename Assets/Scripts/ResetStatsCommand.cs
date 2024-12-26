using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStatsCommand : ICommand
{
    private HeroManager heroManager;

    public ResetStatsCommand(HeroManager heroManager)
    {
        this.heroManager = heroManager;
    }

    public void Execute()
    {
        heroManager.ResetAllStats();
        Debug.Log("Tüm statlar sıfırlandı.");
    }
}
