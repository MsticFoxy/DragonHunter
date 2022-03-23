using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Pawn possessed;

    public string AxisNameLookX = "LookX";
    public string AxisNameLookY = "LookY";
    public string AxisNameMoveX = "MoveX";
    public string AxisNameMoveY = "MoveY";
    public Action<float> AxisLookX { get; private set; }
    public Action<float> AxisLookY { get; private set; }

    public Action<float> AxisMoveX { get; private set; }
    public Action<float> AxisMoveY { get; private set; }

    // Update is called once per frame
    void Update()
    {
        AxisLookX.Invoke(Input.GetAxisRaw(AxisNameLookX));
        AxisLookY.Invoke(Input.GetAxisRaw(AxisNameLookY));
        AxisMoveX.Invoke(Input.GetAxisRaw(AxisNameMoveX));
        AxisMoveY.Invoke(Input.GetAxisRaw(AxisNameMoveY));
    }

    /// <summary>Possesses the given pawn and unpossesses the currently possessed one if existing</summary>
    /// <param name="pawn">The pawn that will be possessed</param>
    public void Possess(Pawn pawn)
    {

    }
}
