using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAutoRun : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer sr;
    public float runSpeed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        transform.position += Vector3.down * runSpeed * Time.deltaTime;

    }
    void FixedUpdate()
    {
        rb.velocity = movement.normalized * runSpeed;
        Debug.Log($"MoveX: {movement.x}, MoveY: {movement.y}");
    }
}