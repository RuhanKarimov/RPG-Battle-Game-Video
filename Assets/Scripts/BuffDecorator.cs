using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuffDecorator : FighterDecorator
{
    private float extraMelee;
    private float extraDefense;
    private float duration;
    private float timer;

    public BuffDecorator(IFighter fighter, float extraMelee, float extraDefense, float duration)
        : base(fighter)
    {
        this.extraMelee = extraMelee;
        this.extraDefense = extraDefense;
        this.duration = duration;

        // Anlık buff uyguluyoruz
        Melee += extraMelee;
        Defense += extraDefense;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            // Buff süresi bitince eski değerlere dön
            Melee -= extraMelee;
            Defense -= extraDefense;
        }
    }
}
