using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn), typeof(CapsuleCollider))]
public class CharacterMovement : MonoBehaviour
{
    protected Pawn pawn { get; private set; }
    protected CapsuleCollider capsuleCollider { get; private set; }

    public Vector3 velocity { get; set; }

    public LayerMask collisionMask;

    public Vector3 gravity = new Vector3(0, 9.81f, 0);

    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<Pawn>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(pawn.GetMovementVector());
    }

    private void FixedUpdate()
    {
        Debug.Log(velocity);
        ApplyGravity();
        PhysicsMoveCapsule();
    }

    private void ApplyGravity()
    {
        velocity += gravity * Time.deltaTime;
    }

    private void PhysicsMoveCapsule()
    {
        Vector3 destination = transform.position + velocity * Time.deltaTime;
        RaycastHit hit;
        if(Physics.CapsuleCast(transform.position + transform.up * capsuleCollider.height * 0.5f,
            transform.position - transform.up * capsuleCollider.height * 0.5f,
            capsuleCollider.radius,
            velocity,
            out hit,
            velocity.magnitude * Time.deltaTime,
            collisionMask))
        {
            transform.position = hit.distance * velocity.normalized;
            velocity = Vector3.ProjectOnPlane(velocity, hit.normal);
            Debug.DrawLine(hit.point, hit.point + Vector3.up);
        }


    }

    private void Move(Vector2 direction)
    {
        transform.position += new Vector3(direction.x,0,direction.y) * Time.deltaTime;
    }
}
