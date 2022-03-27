using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement), typeof(Pawn))]
public class DragonHunterControl : MonoBehaviour
{
    CharacterMovement characterMovement;
    CameraController cameraController;
    Pawn pawn;
    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        cameraController = GetComponent<CameraController>();
        pawn = GetComponent<Pawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pawn.playerController)
        {
            Vector3 moveDirection3D = pawn.playerController.transform.right * pawn.GetMovementVector().x
                + Vector3.ProjectOnPlane(pawn.playerController.transform.forward, Vector3.up).normalized * pawn.GetMovementVector().y;
            characterMovement.Move(new Vector2(moveDirection3D.x,moveDirection3D.z));
        }
    }
}
