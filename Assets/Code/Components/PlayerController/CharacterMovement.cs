using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn))]
public class CharacterMovement : MonoBehaviour
{
    protected Pawn pawn;
    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
