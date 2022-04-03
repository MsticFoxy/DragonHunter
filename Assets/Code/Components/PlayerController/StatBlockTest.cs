using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(StatBlock))]
public class StatBlockTest : MonoBehaviour
{

    private StatBlock statblock;

    // Start is called before the first frame update
    void Start()
    {
        statblock = GetComponent<StatBlock>();
        statblock.Initialize();
        statblock.GetStat<StatValue<float>>("value");
        statblock.AddStatusEffect(0, new StatusEffectBurn(3));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(combine.value.current);
    }

    public StatValue<float> value;
    public StatValue<PoolValueFloat> health = new StatValue<PoolValueFloat>();
    public StatValue<PoolValueInt> testClassInt = new StatValue<PoolValueInt>();
    public CombineStat<PoolValueFloat> combine = new CombineStat<PoolValueFloat>((statblock, stat) => 
    { 
        PoolValueFloat ret = stat;
        ret.max = statblock.GetStat<StatValue<PoolValueFloat>>("health").value.max
                    + statblock.GetStat<StatValue<PoolValueInt>>("testClassInt").value.max;
        ret.min = statblock.GetStat<StatValue<PoolValueFloat>>("health").value.min
                    + statblock.GetStat<StatValue<PoolValueInt>>("testClassInt").value.min;
        ret.current = statblock.GetStat<StatValue<PoolValueFloat>>("health").value.current 
                    + statblock.GetStat<StatValue<PoolValueInt>>("testClassInt").value.current;
        return ret; 
    });

}
