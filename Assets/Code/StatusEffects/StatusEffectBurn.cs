using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectBurn : StatusEffect
{
    StatValue<PoolValueFloat> health;
    List<float> dpsList = new List<float>();

    public StatusEffectBurn(float dps)
    {
        dpsList.Add(dps);
    }

    public override void Begin()
    {
        base.Begin();
        health = owner.GetStat<StatValue<PoolValueFloat>>("health");
        
    }
    public override void Tick()
    {
        base.Tick();

        if (health != null)
        {
            float dmg = 0;
            foreach (float dps in dpsList)
            {
                dmg += dps * Time.deltaTime;
            }
            health.value.current -= dmg;
        }
    }

    public override void End()
    {
        base.End();
    }

    public override bool BlockStatusEffectOnAdditionToOwner(StatusEffect effect)
    {
        if(effect is StatusEffectBurn)
        {
            foreach (float dps in ((StatusEffectBurn)effect).dpsList)
            {
                dpsList.Add(dps);
            }
            return true;
        }
        return base.BlockStatusEffectOnAdditionToOwner(effect);
    }
}
