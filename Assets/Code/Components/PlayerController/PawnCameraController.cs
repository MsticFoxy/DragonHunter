using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn))]
public class PawnCameraController : CameraController
{
    protected Pawn pawn { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    // Update is called once per frame
    void Update()
    {
        pawn.playerController.transform.rotation = Quaternion.LookRotation(
            transform.position - pawn.playerController.transform.position, Vector3.up);
    }
}
