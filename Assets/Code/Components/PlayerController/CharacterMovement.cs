using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn))]
public class CharacterMovement : MonoBehaviour
{
    protected Pawn pawn;

    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(pawn.GetMovementVector());
    }

    private void Move(Vector2 direction)
    {
        transform.position += new Vector3(direction.x,0,direction.y) * Time.deltaTime;
    }
}
