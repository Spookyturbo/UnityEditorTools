using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

    private Movement movement;

    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.Space))
        {
            movement.Jump();
        }
    }

    void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");

        if (xInput > 0)
            movement.MoveRight();
        else if (xInput < 0)
            movement.MoveLeft();
    }
}
