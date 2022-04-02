using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatBase
{
    #region Base Stat Events
    public Action OnAddedToStatBlock;
    #endregion

    #region Stat Value Ownership
    public StatBlock owner;
    #endregion
}
