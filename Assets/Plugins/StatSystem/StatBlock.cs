using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        stats.Add("h", new StatValue<float>());
        StatValue<float> flst = GetStat<StatValue<float>>("h");
        flst.AddModifier(0, new StatModifier<float>((val) => { return val + 5; }));
        Debug.Log("StatValue: " + flst.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Dictionary<string, StatBase> stats = new Dictionary<string, StatBase>();
    private Dictionary<string, List<StatusEffect>> effects;

    /// <summary>
    /// Adds the given stat to this statblock if its name is unique in this statblock.
    /// </summary>
    /// <param name="name">The name of the Stat.</param>
    /// <param name="stat">The Stat thats is going to be added.</param>
    /// <returns>Returns false if there is already a Stat with this name.</returns>
    public bool AddStat(string name, StatBase stat)
    {
        if (stat != null)
        {
            if (!stats.ContainsKey(name))
            {
                stats.Add(name, stat);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Gets the stat with the given name if the types match.
    /// </summary>
    /// <typeparam name="T">The type that the stat type has to match.</typeparam>
    /// <param name="name">The name of the stat.</param>
    /// <returns>Return the stat or a default value of the type if there is no matching stat.</returns>
    public T GetStat<T>(string name) where T : StatBase
    {
        StatBase stat;
        if(stats.TryGetValue(name, out stat))
        {
            if(stat is T)
            {
                return (T)stat;
            }
        }
        return default;
    }

    /// <summary>
    /// Gets the type of the given stat.
    /// </summary>
    /// <param name="name">The name of the stat</param>
    /// <returns>Returns the type of the stat and null if there is no matching stat.</returns>
    public Type GetTypeOfStat(string name)
    {
        StatBase stat;
        if (stats.TryGetValue(name, out stat))
        {
            return stat.GetType();
        }
        return null;
    }
}
