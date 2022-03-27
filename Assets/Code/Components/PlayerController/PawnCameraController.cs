using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn))]
public class PawnCameraController : CameraController
{
    public GameObject target;
    public Vector3 lookDirection;
    public float cameraDistance = 2.0f;
    public float cameraHeight = 0.5f;
    public Vector2 cameraSensitivity = new Vector2(1,1);
    public Vector2 offset;
    protected Pawn pawn { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<Pawn>();
    }
    

    // Update is called once per frame
    void Update()
    {
        lookDirection = Quaternion.AngleAxis(pawn.lookInput.x * cameraSensitivity.x, Vector3.up) * lookDirection;
        //lookDirection = Quaternion.AngleAxis(pawn.lookInput.y * cameraSensitivity.y, pawn.playerController.transform.forward) * lookDirection;
        pawn.playerController.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        pawn.playerController.transform.position = Vector3.up * cameraHeight 
            - Vector3.ProjectOnPlane(lookDirection, Vector3.up).normalized * cameraDistance
            +pawn.transform.position
            +pawn.playerController.transform.right * offset.x
            +Vector3.up * offset.y;
    }

    
}
