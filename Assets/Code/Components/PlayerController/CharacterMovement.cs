using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn), typeof(CapsuleCollider))]
public class CharacterMovement : MonoBehaviour
{
    protected Pawn pawn { get; private set; }
    protected CapsuleCollider capsuleCollider { get; private set; }

    public bool isGrounded { get; private set; }
    public bool isSliding;// { get; private set; }
    public Vector3 velocity { get; set; }

    public LayerMask collisionMask;

    public Vector3 gravity = new Vector3(0, -9.81f, 0);
    public float walkingSlope = 40;
    public float maxStepHeight = 0.2f;
    public float walkingSpeed = 10;
    public float slidingAcceleration = 10.0f;

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
        
    }

    private void FixedUpdate()
    {
        //Debug.Log(velocity);
        ApplyGravity();
        PhysicsMoveCapsule();
    }

    private void ApplyGravity()
    {
        if (!isSliding)
        {
            velocity += gravity * Time.deltaTime;
        }
    }

    private void PhysicsMoveCapsule()
    {
        float capsulePointDistance = (capsuleCollider.height - capsuleCollider.radius*2.0f);
        Vector3 topCapsule = transform.position + transform.up * capsulePointDistance * 0.5f;
        Vector3 bottomCapsule = transform.position - transform.up * capsulePointDistance * 0.5f;
        RaycastHit hit;

        // checks for collisions and applies velocity
        if(Physics.CapsuleCast(topCapsule,
            bottomCapsule,
            capsuleCollider.radius,
            velocity,
            out hit,
            velocity.magnitude * Time.deltaTime,
            collisionMask))
        {
            transform.position += hit.distance * velocity.normalized;
            velocity = FlattenVector(velocity, hit.normal, transform.up, walkingSlope);
            //Debug.DrawLine(hit.point, hit.point + Vector3.up);
        }
        else
        {
            transform.position = transform.position + velocity * Time.deltaTime;
        }

        // checks if grounded and applies velocity changes
        isGrounded = false;
        isSliding = false;
        if(Physics.SphereCast(topCapsule, 
            capsuleCollider.radius, 
            (bottomCapsule - topCapsule).normalized, 
            out hit,
            capsulePointDistance + maxStepHeight,
            collisionMask
            ))
        {
            if(Vector3.Angle(hit.normal, transform.up) <= walkingSlope)
            {
                isGrounded = true;
                velocity = new Vector3(0, velocity.y, 0);
            }
            else
            {
                isSliding = true;
                velocity = Vector3.ProjectOnPlane(velocity, hit.normal) 
                    + Vector3.ProjectOnPlane(gravity, hit.normal).normalized * slidingAcceleration * Time.deltaTime;
            }
        }
        //Debug.Log(isGrounded);

        // forces object to stay out of any collider
        foreach (Collider c in Physics.OverlapCapsule(topCapsule,
            bottomCapsule,
            capsuleCollider.radius,
            collisionMask
            ))
        {
            if (c != capsuleCollider)
            {
                if (Physics.ComputePenetration(capsuleCollider, transform.position, transform.rotation,
                    c, c.transform.position, c.transform.rotation,
                    out Vector3 direction, out float distance
                    ))
                {
                    if (Vector3.Angle(direction, transform.up) <= walkingSlope)
                    {
                        transform.position += transform.up * distance;
                    }
                    else
                    {
                        transform.position += direction * distance;
                    }
                    if (!isSliding)
                    {
                        velocity = FlattenVector(velocity, direction, transform.up, walkingSlope);
                    }
                }
            }
        }

    }

    private Vector3 FlattenVector(Vector3 vector, Vector3 plane, Vector3 up, float slope)
    {
        if(Vector3.Angle(plane, up) <= slope)
        {
            return Vector3.ProjectOnPlane(velocity, up);
        }
        return Vector3.ProjectOnPlane(velocity, plane);
    }

    public void Move(Vector2 direction)
    {
        Vector3 movePosition = transform.position;
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

        float capsulePointDistance = (capsuleCollider.height - capsuleCollider.radius * 2.0f);
        Vector3 topCapsule = transform.position + (transform.up * capsulePointDistance * 0.5f);
        Vector3 bottomCapsule = transform.position - (transform.up * capsulePointDistance * 0.5f);
        List<Vector3> hitPositions = new List<Vector3>();
        Vector3 endPosition = transform.position + moveDirection.normalized * walkingSpeed * Time.deltaTime;
        Vector3 prevPosition = transform.position;
        foreach(RaycastHit h in Physics.CapsuleCastAll(topCapsule - moveDirection.normalized * 0.01f,
            bottomCapsule - moveDirection.normalized * 0.01f,
            capsuleCollider.radius,
            moveDirection.normalized,
            walkingSpeed * Time.deltaTime + 0.01f,
            collisionMask
            ))
        {
            if (h.distance > 0)
            {
                Debug.DrawLine(h.point, h.point + h.normal, Color.blue, 5);
                if (Vector3.Angle(h.normal, transform.up) <= walkingSlope)
                {
                    hitPositions.Add(transform.position + moveDirection.normalized * h.distance);
                }
                else
                {
                    endPosition = transform.position + Vector3.ProjectOnPlane(moveDirection.normalized, h.normal) * h.distance;
                    break;
                }
            }
        }
        RaycastHit hit;
        Debug.DrawLine(endPosition, endPosition + transform.up, Color.red, 5);
        if (Physics.SphereCast(endPosition - transform.up * capsulePointDistance * 0.5f + transform.up * maxStepHeight,
            capsuleCollider.radius,
            -transform.up,
            out hit,
            (endPosition - transform.position).magnitude * 10.0f + capsulePointDistance
            ))
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.green, 5);
            transform.position = (endPosition - transform.up * capsulePointDistance * 0.5f) - transform.up * (hit.distance - (capsuleCollider.radius + maxStepHeight));
        }
        else
        {
            transform.position = endPosition;
        }
    }
}
