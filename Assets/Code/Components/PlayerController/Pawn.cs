using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public Vector2 moveInput { get; protected set; }
    public Vector2 lookInput { get; protected set; }

    public PlayerController playerController { get; private set; }

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
        moveInput = new Vector2(axis, moveInput.y);
    }

    protected virtual void MoveY(float axis)
    {
        moveInput = new Vector2(moveInput.x, axis);
    }

    protected virtual void LookX(float axis)
    {
        lookInput = new Vector2(axis, lookInput.y);
    }

    protected virtual void LookY(float axis)
    {
        lookInput = new Vector2(lookInput.x, axis);
    }
    #endregion

    #region Possession Functions
    public void PossessBy(PlayerController playerController)
    {
        if (playerController)
        {
            if (playerController.possessed == this)
            {
                if (this.playerController)
                {
                    this.playerController.Unpossess();
                }
                this.playerController = playerController;
                PossessionBindings(playerController);
            }
        }
    }

    public void UnpossessBy(PlayerController playerController)
    {
        if (playerController)
        {
            if (playerController.possessed == this)
            {
                ClearPossessionBindings(playerController);
                this.playerController = null;
            }
        }
    }

    private void PossessionBindings(PlayerController playerController)
    {
        playerController.AxisMoveX += MoveX;
        playerController.AxisMoveY += MoveY;
        playerController.AxisLookX += LookX;
        playerController.AxisLookY += LookY;
    }
    private void ClearPossessionBindings(PlayerController playerController)
    {
        playerController.AxisMoveX -= MoveX;
        playerController.AxisMoveY -= MoveY;
        playerController.AxisLookX -= LookX;
        playerController.AxisLookY -= LookY;
    }
    #endregion

    #region Movement Vectors
    public Vector2 GetMovementVector()
    {
        if(moveInput.magnitude > 1)
        {
            return moveInput.normalized;
        }
        return moveInput; 
    }

    public Vector2 GetLookVector()
    {
        return lookInput;
    }
    #endregion
}
