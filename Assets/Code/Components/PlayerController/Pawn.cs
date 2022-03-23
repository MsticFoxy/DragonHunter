using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private float xMoveInput { get; set; }
    private float yMoveInput { get; set; }

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Input Functions
    protected virtual void MoveX(float axis)
    {
        xMoveInput = axis;
    }

    protected virtual void MoveY(float axis)
    {
        yMoveInput = axis;
    }

    protected virtual void LookX(float axis)
    {

    }

    protected virtual void LookY(float axis)
    {

    }
    #endregion
}
