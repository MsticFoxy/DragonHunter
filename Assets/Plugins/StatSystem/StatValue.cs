using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier<T>
{
    protected Func<T, T> modification;

    /// <summary>
    /// Creates a modifier with the given modification.
    /// </summary>
    /// <param name="modification">The modification that gets the value previous to this 
    /// modification and returns the modified value.</param>
    public StatModifier(Func<T, T> modification)
    {
        this.modification = modification;
    }

    public T Apply(T stat)
    {
        return modification(stat);
    }
}

public class StatValue<T> : StatBase
{
    #region Stat Value Properties
    private T _baseValue;
    public T baseValue
    {
        get
        {
            return _baseValue;
        }
        private set
        {
            _baseValue = value;
            Invalidate();
        }
    }

    private T _value;
    public T value 
    { 
        get
        {
            if(_isDirty)
            {
                _value = CalculateValue();
            }
            return _value;
        }
        private set
        {
            _value = value;
        }
    }

    private Dictionary<int, List<StatModifier<T>>> modifiers = new Dictionary<int, List<StatModifier<T>>>();
    #endregion

    #region Stat State Properties
    private bool _isDirty;
    #endregion

    /// <summary>
    /// Adds a modifier to this statvalue wich will be applied with the given priority.
    /// </summary>
    /// <param name="priority">The priority with wich the modifier will be applied.</param>
    /// <param name="modifier">The modifier that will be applied to this stat.</param>
    public void AddModifier(int priority, StatModifier<T> modifier)
    {
        if(modifiers.ContainsKey(priority))
        {
            List<StatModifier<T>> prioMods;
            modifiers.TryGetValue(priority, out prioMods);
            prioMods.Add(modifier);
        }
        else
        {
            List<StatModifier<T>> newPrio = new List<StatModifier<T>>();
            newPrio.Add(modifier);
            modifiers.Add(priority, newPrio);
        }
        Invalidate();
    }

    /// <summary>
    /// Removes the given modifier from this stat.
    /// </summary>
    /// <param name="modifier">The modifier that will be removed</param>
    public void RemoveModifier(StatModifier<T> modifier)
    {
        foreach(KeyValuePair<int, List<StatModifier<T>>> entry in modifiers)
        {
            Debug.Log(entry.Value);
            if(entry.Value.Contains(modifier))
            {
                entry.Value.Remove(modifier);
                if(entry.Value.Count == 0)
                {
                    modifiers.Remove(entry.Key);
                    Invalidate();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Marks the stat as dirty so it has to recalculate the next time it is read.
    /// </summary>
    protected void Invalidate()
    {
        _isDirty = true;
    }

    /// <summary>
    /// Calculates the stat and applies all modifiers.
    /// </summary>
    /// <returns>Returns the modified stat.</returns>
    protected T CalculateValue()
    {
        T valueCopy = _baseValue;
        if (typeof(ICloneable).IsAssignableFrom(typeof(T)))
        {
            valueCopy = (T)((ICloneable)_baseValue).Clone();
        }

        foreach (KeyValuePair<int, List<StatModifier<T>>> entry in modifiers)
        {
            if(entry.Value != null)
            {
                foreach(StatModifier<T> mod in entry.Value)
                {
                    valueCopy = mod.Apply(valueCopy);
                }
            }
        }

        return valueCopy;
    }
}
