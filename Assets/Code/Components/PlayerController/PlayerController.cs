using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Pawn possessOnStart;
    public Pawn possessed { get; private set; }

    public string AxisNameLookX = "LookX";
    public string AxisNameLookY = "LookY";
    public string AxisNameMoveX = "MoveX";
    public string AxisNameMoveY = "MoveY";
    public Action<float> AxisLookX { get; set; }
    public Action<float> AxisLookY { get; set; }

    public Action<float> AxisMoveX { get; set; }
    public Action<float> AxisMoveY { get; set; }
    private void Start()
    {
        if(possessOnStart)
        {
            Possess(possessOnStart);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (AxisLookX != null)
        {
            AxisLookX.Invoke(Input.GetAxisRaw(AxisNameLookX));
        }
        if (AxisLookY != null)
        {
            AxisLookY.Invoke(Input.GetAxisRaw(AxisNameLookY));
        }
        if (AxisMoveX != null)
        {
            AxisMoveX.Invoke(Input.GetAxisRaw(AxisNameMoveX));
        }
        if (AxisMoveY != null)
        {
            AxisMoveY.Invoke(Input.GetAxisRaw(AxisNameMoveY));
        }
    }

    /// <summary>Possesses the given pawn and unpossesses the currently possessed one if existing</summary>
    /// <param name="pawn">The pawn that will be possessed</param>
    public void Possess(Pawn pawn)
    {
        possessed = pawn;
        pawn.PossessBy(this);
    }

    public void Unpossess()
    {
        if(possessed)
        {
            possessed.UnpossessBy(this);
            possessed = null;
        }
    }
}
