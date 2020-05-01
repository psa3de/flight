using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool transformed;
    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        transformed = false;
    }

    protected override void ComputeVelocity()
    {
        if (Input.GetButtonDown("Transform"))
        {
            Debug.Log("Transform");
            transformed = !transformed;
        }
        Vector2 move = Vector2.zero;
        if(transformed)
        {
            // If grounded and transform, stop moving
            if (grounded)
            {
                velocity.x = 0;
                velocity.y = 0;
            }
            // If not grounded and transform, continue moving
            else
            {

            }
        }
        else {
            // If not transform, move normally
            move.x = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }

            velocity.x = move.x * maxSpeed;
        }
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        SetAnimatorValues();

    }

    void SetAnimatorValues()
    {
        animator.SetBool("grounded", grounded);
        animator.SetFloat("magnitudeX", Mathf.Abs(velocity.x));
        animator.SetBool("transformed", transformed);
        animator.SetFloat("velocityY", velocity.y);
    }

}