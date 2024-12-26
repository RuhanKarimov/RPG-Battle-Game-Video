using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class FighterDecorator : IFighter
{
    protected IFighter wrappedFighter;

    protected FighterDecorator(IFighter fighter)
    {
        wrappedFighter = fighter;
    }

    public virtual float Health { get => wrappedFighter.Health; set => wrappedFighter.Health = value; }
    public virtual float Magic { get => wrappedFighter.Magic; set => wrappedFighter.Magic = value; }
    public virtual float Melee { get => wrappedFighter.Melee; set => wrappedFighter.Melee = value; }
    public virtual float MagicRange { get => wrappedFighter.MagicRange; set => wrappedFighter.MagicRange = value; }
    public virtual float Defense { get => wrappedFighter.Defense; set => wrappedFighter.Defense = value; }
    public virtual float Speed { get => wrappedFighter.Speed; set => wrappedFighter.Speed = value; }
    public virtual float Experience { get => wrappedFighter.Experience; set => wrappedFighter.Experience = value; }

    public virtual void ReceiveDamage(float damage)
    {
        wrappedFighter.ReceiveDamage(damage);
    }

    public virtual bool GetDead()
    {
        return wrappedFighter.GetDead();
    }

    public virtual void UpdateStat(string statName, float amount)
    {
        wrappedFighter.UpdateStat(statName, amount);
    }

    public virtual void CalculateNextTurn(int currentTurn)
    {
        wrappedFighter.CalculateNextTurn(currentTurn);
    }
}
