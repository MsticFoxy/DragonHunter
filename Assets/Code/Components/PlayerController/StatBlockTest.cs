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
        statblock.GetStat<StatValue<float>>("value");
        statblock.AddStatusEffect(0, new StatusEffectBurn(3));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public StatValue<float> value;
    public StatValue<PoolValueFloat> health = new StatValue<PoolValueFloat>();
    public StatValue<PoolValueInt> testClassInt;
    public CombineStat<PoolValueFloat> combine = new CombineStat<PoolValueFloat>(new List<string>() { }, () => 
    { 

        return default; 
    });

}
