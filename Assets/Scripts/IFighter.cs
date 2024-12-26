using System.Collections;
using System.Collections.Generic;
using UnityEngine;// IFighter.cs
public interface IFighter
{
    float Health { get; set; }
    float Magic { get; set; }
    float Melee { get; set; }
    float MagicRange { get; set; }
    float Defense { get; set; }
    float Speed { get; set; }
    float Experience { get; set; }

    void ReceiveDamage(float damage);
    bool GetDead();
    void UpdateStat(string statName, float amount);
    void CalculateNextTurn(int currentTurn);
}
