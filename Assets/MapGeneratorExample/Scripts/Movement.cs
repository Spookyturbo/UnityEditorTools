using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Inspector stuff
    public float speed = 100;
    public float jumpForce = 100;
    public float distanceToGround;
    public LayerMask groundMask;

    //Components
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer character;

    //Variables

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        character = GetComponentInChildren<SpriteRenderer>();
    }

    public void MoveRight()
    {
        Move(Vector2.right * speed);                 
    }

    public void MoveLeft()
    {
        Move(Vector2.left * speed);
    }

    private void Move(Vector2 force)
    {
        //Add force to move and notify the animator
        rb.AddForce(force);

        //Face direction
        if(rb.velocity.x > 0)
        {
            character.flipX = false;
        }
        else if(rb.velocity.x < 0)
        {
            character.flipX = true;
        }
    }

    public void Jump()
    {        
        if (Grounded())
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private void FixedUpdate()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        //call to check for ground to update the animator
        Grounded();
    }

    private bool Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,
            distanceToGround, groundMask);
        if (hit.collider == null)
        {
            anim.SetBool("Ground", false);
            return false;
        }
        anim.SetBool("Ground", true);
        return true;
    }
}
