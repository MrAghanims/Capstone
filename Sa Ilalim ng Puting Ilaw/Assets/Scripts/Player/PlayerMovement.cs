using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

        if (movement.x != 0)
        {
            sr.flipX = movement.x < 0;
        }
        if (Mathf.Abs(movement.x) < 0.1f) movement.x = 0;
        if (Mathf.Abs(movement.y) < 0.1f) movement.y = 0;
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
        //Debug.Log($"MoveX: {movement.x}, MoveY: {movement.y}");
    }

}